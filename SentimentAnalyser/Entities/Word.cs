namespace SentimentAnalyser.Entities
{
    public class Word
    {
        public int WordId { get; set; }
        public string WordDesc { get; set; }
        public double SentimentScore { get; set; }
    }
}
