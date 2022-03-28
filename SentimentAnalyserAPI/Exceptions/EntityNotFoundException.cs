using System;

namespace SentimentAnalyserAPI.Exceptions
{
    /// <summary>
    /// Entity Not Found Exception class
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException" /> class
        /// </summary>
        /// <param name="message">Exception message</param>
        public EntityNotFoundException(string message) : base(message) { }
    }
}
