using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using SentimentAnalyserAPI.Exceptions;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalyserAPI.Security
{
    /// <summary>
    /// Anti XSS Middleware class
    /// </summary>
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="AntiXssMiddleware"/> class.
        /// </summary>
        /// <param name="next">Request delegate</param>
        public AntiXssMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Sanitises and validates the entire request body through middleware.
        /// </summary>
        /// <param name="httpContext">Http Context</param>
        public async Task Invoke(HttpContext httpContext)
        {
            // Enable buffering so that the request can be read by the model binders next
            httpContext.Request.EnableBuffering();

            // LeaveOpen: true to leave the stream open after disposing,
            // so it can be read by the model binders
            using (var streamReader = new StreamReader
                  (httpContext.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var raw = await streamReader.ReadToEndAsync();
                var sanitiser = new HtmlSanitizer();
                var sanitised = sanitiser.Sanitize(raw);

                if (raw != sanitised)
                {
                    throw new BadRequestException("XSS injection detected from middleware.");
                }
            }

            // Rewind the stream for the next middleware
            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            await _next.Invoke(httpContext);
        }
    }
}
