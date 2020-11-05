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
    public class RacesController : ControllerBase
    {
        private readonly IRaceService _raceService;
        private readonly ILoggerAdapter<RacesController> _logger;

        public RacesController(
            IRaceService raceService,
            ILoggerAdapter<RacesController> logger
        )
        {
            _logger = logger;
            _raceService = raceService;
        }

        // GET: api/Races
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RacesResult>>> GetAll()
        {
            try
            {
                var result = await _raceService.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Races");
        }

        // GET: api/Races/5
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RaceResult>> Get(byte id)
        {
            try
            {
                var result = await _raceService.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Race");
        }

        // GET: api/Races/5/guid
        [HttpPost("{raceId}/{horseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RaceRunResult>> Race(byte raceId, Guid horseId)
        {
            try
            {
                var result = await _raceService.Race(raceId, horseId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to run Race");
        }
    }
}
