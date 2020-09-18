using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Interfaces.Logging;
using TripleDerby.Core.Interfaces.Services;

namespace TripleDerby.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedingsController : ControllerBase
    {
        private readonly IFeedingService _feedingService;
        private readonly ILoggerAdapter<FeedingsController> _logger;

        public FeedingsController(
            IFeedingService feedingService,
            ILoggerAdapter<FeedingsController> logger
        )
        {
            _logger = logger;
            _feedingService = feedingService;
        }

        // GET: api/Feedings
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FeedingsResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _feedingService.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Feedings");
        }

        // GET: api/Feedings/5
        [HttpGet("{feedingId}")]
        [ProducesResponseType(typeof(FeedingResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(byte feedingId)
        {
            try
            {
                var result = await _feedingService.Get(feedingId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Feeding");
        }

        // GET: api/Feedings/5/guid
        [HttpPost("{feedingId}/{horseId}")]
        [ProducesResponseType(typeof(FeedingSessionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Feed(byte feedingId, Guid horseId)
        {
            try
            {
                var result = await _feedingService.Feed(feedingId, horseId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to create Feeding Session");
        }
    }
}
