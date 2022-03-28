using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SentimentAnalyserAPI.Entities;
using SentimentAnalyserAPI.Enums;
using SentimentAnalyserAPI.Exceptions;
using SentimentAnalyserAPI.Repositories;

namespace SentimentAnalyserAPI.Controllers
{
    /// <summary>
    /// Default application controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LexiconController : ControllerBase
    {
        private readonly ILogger<LexiconController> _logger;
        private readonly ILexiconRepository _lexiconRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LexiconController"/> class.
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="lexiconRepository">Lexicon Repository</param>
        public LexiconController(ILogger<LexiconController> logger, ILexiconRepository lexiconRepository)
        {
            _logger = logger;
            _lexiconRepository = lexiconRepository;
        }

        /// <summary>
        /// Returns words by provided filter id
        /// </summary>
        /// <param name="filterId">Filter Id</param>
        /// <returns>Http response</returns>
        /// <remarks>
        /// Returns Words collection<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        [HttpGet]
        [Route("")]
        public IActionResult GetFilteredWords(FilterEnum filterId = FilterEnum.All)
        {
            var result = _lexiconRepository.GetFilteredWords(filterId);
            return Ok(result);
        }

        /// <summary>
        /// Delete word by provided word id
        /// </summary>
        /// <param name="wordId">Word Id</param>
        /// <returns>Http response</returns>
        /// <remarks>
        /// Deletes word<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        [HttpDelete]
        [Route("{wordId}")]
        public IActionResult DeleteWord(int wordId)
        {
            try
            {
                _lexiconRepository.DeleteWord(wordId);
                return Ok();
            }
            catch (EntityException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add new word or edit existing one
        /// </summary>
        /// <param name="wordModel">Word model</param>
        /// <returns>Http response</returns>
        /// <remarks>
        /// Add or Edit word<br/>
        /// Depends on <b>Lexicon table</b>
        /// </remarks>
        [HttpPost]
        [Route("")]
        public IActionResult AddEditWord([FromBody] Word wordModel)
        {
            try
            {
                var result = _lexiconRepository.AddEditWord(wordModel);
                return Ok(result);
            }
            catch (EntityException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
