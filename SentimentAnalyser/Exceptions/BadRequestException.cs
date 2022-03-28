using System;

namespace SentimentAnalyser.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(ExceptionModel em) : base(em.Message) { }

        public BadRequestException(string message) : base(message) { }
    }
}
