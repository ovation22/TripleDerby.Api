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
    public class BreedingController : ControllerBase
    {
        private readonly IBreedingService _breedingService;
        private readonly ILoggerAdapter<BreedingController> _logger;

        public BreedingController(
            IBreedingService breedingService,
            ILoggerAdapter<BreedingController> logger
        )
        {
            _logger = logger;
            _breedingService = breedingService;
        }

        // GET: api/Breeding/Dams
        [HttpGet("Dams")]
        [ProducesResponseType(typeof(IEnumerable<ParentHorse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetDams()
        {
            try
            {
                var result = await _breedingService.GetDams();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Dams");
        }

        // GET: api/Breeding/Sires
        [HttpGet("Sires")]
        [ProducesResponseType(typeof(IEnumerable<ParentHorse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetSires()
        {
            try
            {
                var result = await _breedingService.GetSires();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Sires");
        }

        // GET: api/Breeding
        [HttpPost]
        [ProducesResponseType(typeof(Foal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Breed([FromBody] BreedRequest request)
        {
            try
            {
                var result = await _breedingService.Breed(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Create Foal");
        }
    }
}
