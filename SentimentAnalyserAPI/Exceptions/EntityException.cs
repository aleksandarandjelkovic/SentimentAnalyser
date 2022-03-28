using System;

namespace SentimentAnalyserAPI.Exceptions
{
    /// <summary>
    /// Entity Exception class
    /// </summary>
    public class EntityException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityException" /> class
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="inner">Inner exception</param>
        public EntityException(string message, Exception inner) : base(message, inner) { }
    }
}
