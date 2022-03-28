using SentimentAnalyserAPI.Entities;
using SentimentAnalyserAPI.Enums;
using System.Collections.Generic;

namespace SentimentAnalyserAPI.Repositories
{
    /// <summary>
    /// Lexicon Repository interface
    /// </summary>
    public interface ILexiconRepository
    {
        /// <summary>
        /// Returns words by provided filter id
        /// </summary>
        /// <param name="filterId">Filter Id</param>
        /// <remarks>
        /// Returns Words collection<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        IEnumerable<Word> GetFilteredWords(FilterEnum filterId);

        /// <summary>
        /// Returns words by provided word id
        /// </summary>
        /// <param name="wordId">Word Id</param>
        /// <remarks>
        /// Returns single word by word id<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        Word GetWordById(int wordId);

        /// <summary>
        /// Returns words by provided word description
        /// </summary>
        /// <param name="wordDesc">Word Description</param>
        /// <remarks>
        /// Returns single word by word description<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        Word GetWordByDesc(string wordDesc);

        /// <summary>
        /// Delete word by provided word id
        /// </summary>
        /// <param name="wordId">Word Id</param>
        /// <remarks>
        /// Deletes word<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        void DeleteWord(int wordId);

        /// <summary>
        /// Add new word or edit existing one
        /// </summary>
        /// <param name="word">Word model</param>
        /// <returns>Word Id</returns>
        /// <remarks>
        /// Add or Edit word<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        int AddEditWord(Word word);
    }
}
