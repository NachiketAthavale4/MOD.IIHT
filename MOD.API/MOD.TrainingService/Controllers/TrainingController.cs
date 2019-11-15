using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOD.TrainingService.Helpers;
using MOD.TrainingService.Models;
using MOD.TrainingService.Repositories;
using UnprocessableEntityObjectResult = MOD.TrainingService.Helpers.UnprocessableEntityObjectResult;

namespace MOD.TrainingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingRepository trainingRepository;
        private readonly ILogger<TrainingController> logger;

        public TrainingController(ITrainingRepository trainingRepository,
            ILogger<TrainingController> logger)
        {
            this.trainingRepository = trainingRepository;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Training>> Get()
        {
            TrainingResponse response = new TrainingResponse();

            try
            {
                var trainingsReturned = trainingRepository.GetAllTrainings();
                response.TrainingsList = trainingsReturned;
                response.SuccessIndicator = true;
                logger.LogInformation(200, "SuccessIndicator = true");
                return Ok(response);
            }
            catch(Exception ex)
            {
                var faultObj = new Fault()
                {
                    FaultMessage = ex.Message,
                    FaultSource = ex.Source
                };

                if (ex.InnerException != null
                    && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    faultObj.InnerMessage = ex.InnerException.Message;
                }

                response.Fault = faultObj;
                response.SuccessIndicator = false;
                logger.LogError(500, faultObj.FaultMessage);
                logger.LogInformation(500, "sucessIndicator = false");

                return new InternalServerErrorObjectResult(response);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Training> Get(int id)
        {
            TrainingResponse response = new TrainingResponse();

            try
            {
                var trainingFound = trainingRepository.GetTrainingById(id);
                if(trainingFound == null)
                {
                    logger.LogInformation(200, "No Records Found");
                    return NotFound();
                }
                response.TrainingsList = new List<Training>() { trainingFound };
                response.SuccessIndicator = true;
                logger.LogInformation(200, "SuccessIndicator = true");
                return Ok(response);
            }
            catch(Exception ex)
            {
                var faultObj = new Fault()
                {
                    FaultMessage = ex.Message,
                    FaultSource = ex.Source
                };

                if (ex.InnerException != null
                    && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    faultObj.InnerMessage = ex.InnerException.Message;
                }

                response.Fault = faultObj;
                response.SuccessIndicator = false;
                logger.LogError(500, faultObj.FaultMessage);
                logger.LogInformation(500, "sucessIndicator = false");

                return new InternalServerErrorObjectResult(response);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Training training)
        {
            if(training == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            TrainingResponse response = new TrainingResponse()
            {
                TrainingsList = new List<Training>()
                {
                    training
                }
            };

            try
            {
                if (trainingRepository.Save(training) > 0)
                {
                    response.SuccessIndicator = true;
                    logger.LogInformation(200, "SuccessIndicator = true");
                    return Ok(response);
                }
                return new InternalServerErrorObjectResult(response);
            }
            catch (Exception ex)
            {
                var faultObj = new Fault()
                {
                    FaultMessage = ex.Message,
                    FaultSource = ex.Source
                };

                if (ex.InnerException != null
                    && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    faultObj.InnerMessage = ex.InnerException.Message;
                }

                response.Fault = faultObj;
                response.SuccessIndicator = false;
                logger.LogError(500, faultObj.FaultMessage);
                logger.LogInformation(500, "sucessIndicator = false");

                return new InternalServerErrorObjectResult(response);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Training training)
        {
            if (training == null)
            {
                logger.LogInformation(400, "Bad Request");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                logger.LogInformation(422, "Validation error");
                return new UnprocessableEntityObjectResult(ModelState);
            }

            TrainingResponse response = new TrainingResponse()
            {
                TrainingsList = new List<Training>()
                {
                    training
                }
            };

            try
            {
                if (trainingRepository.Update(training) > 0)
                {
                    response.SuccessIndicator = true;
                    logger.LogInformation(200, "SuccessIndicator = true");
                    return Ok(response);
                }
                return new InternalServerErrorObjectResult(response);
            }
            catch (Exception ex)
            {
                var faultObj = new Fault()
                {
                    FaultMessage = ex.Message,
                    FaultSource = ex.Source
                };

                if (ex.InnerException != null
                    && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    faultObj.InnerMessage = ex.InnerException.Message;
                }

                response.Fault = faultObj;
                response.SuccessIndicator = false;
                logger.LogError(500, faultObj.FaultMessage);
                logger.LogInformation(500, "sucessIndicator = false");

                return new InternalServerErrorObjectResult(response);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TrainingResponse response = new TrainingResponse();

            try
            {
                if (trainingRepository.Delete(id))
                {
                    response.SuccessIndicator = true;
                    logger.LogInformation(200, "SuccessIndicator = true");
                    return Ok(response);
                }
                logger.LogInformation(200, "No Records Found");
                return NotFound();
            }
            catch(Exception ex)
            {
                var faultObj = new Fault()
                {
                    FaultMessage = ex.Message,
                    FaultSource = ex.Source
                };

                if (ex.InnerException != null
                    && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    faultObj.InnerMessage = ex.InnerException.Message;
                }

                response.Fault = faultObj;
                response.SuccessIndicator = false;
                logger.LogError(500, faultObj.FaultMessage);
                logger.LogInformation(500, "sucessIndicator = false");

                return new InternalServerErrorObjectResult(response);
            }
        }
    }
}
