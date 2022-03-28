namespace SentimentAnalyserAPI.Enums
{
    /// <summary>
    /// Filter Enum
    /// </summary>
    public enum FilterEnum
    {
        /// <summary>
        /// Filter Id that is used when user wants
        /// to return all words
        /// </summary>
        All = 0,

        /// <summary>
        /// Filter Id that is used when user wants 
        /// to return only words with positive sentiment rating
        /// </summary>
        Positive = 1,

        /// <summary>
        /// Filter Id that is used when user wants 
        /// to return only words with neutral sentiment rating
        /// </summary>
        Neutral = 2,

        /// <summary>
        /// Filter Id that is used when user wants 
        /// to return only words with negative sentiment rating
        /// </summary>
        Negative = 3
    }
}
