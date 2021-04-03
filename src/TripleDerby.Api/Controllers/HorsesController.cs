using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using TripleDerby.Core.DTOs;
using TripleDerby.Core.Interfaces.Logging;
using TripleDerby.Core.Interfaces.Services;

namespace TripleDerby.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorsesController : ControllerBase
    {
        private readonly IHorseService _horseService;
        private readonly ILoggerAdapter<HorsesController> _logger;

        public HorsesController(
            IHorseService horseService,
            ILoggerAdapter<HorsesController> logger
        )
        {
            _logger = logger;
            _horseService = horseService;
        }

        // GET: api/Horses
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HorsesResult>> GetAll(int pageIndex = 0, int itemsPage = 100)
        {
            try
            {
                var result = await _horseService.GetAll(pageIndex, itemsPage);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Horses");
        }

        // GET: api/Horses/5
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HorseResult>> Get(Guid id)
        {
            try
            {
                var result = await _horseService.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Horse");
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<HorsePatch> patch)
        {
            try
            {
                await _horseService.Update(id, patch);

                return new NoContentResult();
            }
            catch (JsonPatchException ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            
            return BadRequest("Unable to update Horse");
        }
    }
}
