using System;

namespace SentimentAnalyserAPI.Exceptions
{
    /// <summary>
    /// Bad Request Exception class
    /// </summary>
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException" /> class
        /// </summary>
        /// <param name="message">Exception message</param>
        public BadRequestException(string message) : base(message) { }
    }
}
