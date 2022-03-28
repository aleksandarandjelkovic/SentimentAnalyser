using System.ComponentModel.DataAnnotations;

namespace SentimentAnalyserAPI.Entities
{
    /// <summary>
    /// Class <c>Word</c>
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Word unique identifier
        /// </summary>
        public int WordId { get; set; }

        /// <summary>
        /// Word description
        /// </summary>
        public string WordDesc { get; set; }

        /// <summary>
        /// Sentiment score represents words sentiment rating
        /// </summary>
        public double SentimentScore { get; set; }
    }
}
