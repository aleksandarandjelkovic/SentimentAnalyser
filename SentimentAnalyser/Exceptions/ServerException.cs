using System;
using System.Net;

namespace SentimentAnalyser.Exceptions
{
    public class ServerException : Exception
    {
        public ServerException(string message, HttpStatusCode statusCode) : base(message)
        {
            HttpStatus = statusCode;
        }

        public HttpStatusCode HttpStatus { get; private set; }
    }
}
