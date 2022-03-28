using SentimentAnalyser.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentimentAnalyser.Services
{
    public class ServiceProxy
    {
        public static async Task<IList<Word>> GetFilteredWords(int filterId = 0)
        {
            return await HttpHelper.Get<IList<Word>>($"lexicon?filterId={filterId}");
        }

        public static async Task DeleteWord(int wordId)
        {
            await HttpHelper.Delete($"lexicon/{wordId}");
        }

        public static async Task<int> AddEditWord(Word word)
        {
            return await HttpHelper.Post<int>("lexicon/", word);
        }
    }
}
