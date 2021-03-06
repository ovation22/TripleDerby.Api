﻿using System;
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
    public class TrainingsController : ControllerBase
    {
        private readonly ITrainingService _trainingService;
        private readonly ILoggerAdapter<TrainingsController> _logger;

        public TrainingsController(
            ITrainingService trainingService,
            ILoggerAdapter<TrainingsController> logger
        )
        {
            _logger = logger;
            _trainingService = trainingService;
        }

        // GET: api/Trainings
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TrainingsResult>>> GetAll()
        {
            try
            {
                var result = await _trainingService.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Trainings");
        }

        // GET: api/Trainings/5
        [HttpGet("{trainingId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TrainingResult>> Get(byte id)
        {
            try
            {
                var result = await _trainingService.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Training");
        }

        // GET: api/Trainings/5/guid
        [HttpPost("{trainingId}/{horseId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TrainingSessionResult>> Train(byte trainingId, Guid horseId)
        {
            try
            {
                var result = await _trainingService.Train(trainingId, horseId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to create Training Session");
        }
    }
}
