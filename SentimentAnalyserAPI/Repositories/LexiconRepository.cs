using SentimentAnalyserAPI.DBContext;
using SentimentAnalyserAPI.Entities;
using SentimentAnalyserAPI.Enums;
using SentimentAnalyserAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SentimentAnalyserAPI.Repositories
{
    /// <summary>
    /// Lexicon Repository class
    /// </summary>
    public class LexiconRepository : ILexiconRepository
    {
        private readonly AppDBContext _appDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LexiconRepository"/> class.
        /// </summary>
        /// <param name="appDbContext">App DB Context</param>
        public LexiconRepository(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Returns words by provided filter id
        /// </summary>
        /// <param name="filterId">Filter Id</param>
        /// <remarks>
        /// Returns Words collection<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        public IEnumerable<Word> GetFilteredWords(FilterEnum filterId)
        {
            switch (filterId)
            {
                case FilterEnum.Positive:
                    return _appDbContext.Words.Where(x => x.SentimentScore > 0).ToArray();

                case FilterEnum.Neutral:
                    return _appDbContext.Words.Where(x => x.SentimentScore == 0).ToArray();

                case FilterEnum.Negative:
                    return _appDbContext.Words.Where(x => x.SentimentScore < 0).ToArray();

                default:
                    return _appDbContext.Words.ToArray();
            }
        }

        /// <summary>
        /// Returns words by provided word id
        /// </summary>
        /// <param name="wordId">Word Id</param>
        /// <remarks>
        /// Returns single word by word id<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        public Word GetWordById(int wordId)
        {
            Word word = _appDbContext.Words.Where(x => x.WordId == wordId).FirstOrDefault();
            if (word == null)
            {
                throw new EntityNotFoundException($"Word with ID={wordId} can not be found.");
            }

            return word;
        }

        /// <summary>
        /// Returns words by provided word description
        /// </summary>
        /// <param name="wordDesc">Word Description</param>
        /// <remarks>
        /// Returns single word by word description<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        public Word GetWordByDesc(string wordDesc)
        {
            Word word = _appDbContext.Words.Where(x => x.WordDesc == wordDesc).FirstOrDefault();
            if (word == null)
            {
                throw new EntityNotFoundException($"Word with description={wordDesc} can not be found.");
            }

            return word;
        }

        /// <summary>
        /// Delete word by provided word id
        /// </summary>
        /// <param name="wordId">Word Id</param>
        /// <remarks>
        /// Deletes word<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        public void DeleteWord(int wordId)
        {
            try
            {
                var wordToDelete = GetWordById(wordId);
                if (wordToDelete != null)
                {
                    _appDbContext.Words.Remove(wordToDelete);
                    _appDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // TODO: log original error
                throw new EntityException("Deleting word failed.", ex);
            }
        }

        /// <summary>
        /// Add new word or edit existing one
        /// </summary>
        /// <param name="word">Word model</param>
        /// <returns>Word Id</returns>
        /// <remarks>
        /// Add or Edit word<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        public int AddEditWord(Word word)
        {
            try
            {
                if (word.WordId == 0)
                {
                    _appDbContext.Add(word);
                }
                else
                {
                    _appDbContext.Update(word);
                }

                _appDbContext.SaveChanges();
                return word.WordId;
            }
            catch (Exception ex)
            {
                // TODO: log original error
                throw new EntityException("Saving word failed.", ex);
            }
        }
    }
}
