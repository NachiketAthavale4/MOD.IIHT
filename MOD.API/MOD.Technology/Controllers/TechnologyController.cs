using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOD.TechnologyService.Helpers;
using MOD.TechnologyService.Models;
using MOD.TechnologyService.Repositories;
using UnprocessableEntityObjectResult = MOD.TechnologyService.Helpers.UnprocessableEntityObjectResult;

namespace MOD.TechnologyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TechnologyController : ControllerBase
    {
        private readonly ITechnologyRepository _repository;
        private readonly ILogger<TechnologyController> logger;

        public TechnologyController(ITechnologyRepository repository,
            ILogger<TechnologyController> logger)
        {
            this._repository = repository;
            this.logger = logger;
        }
        
        [HttpGet]
        public ActionResult<TechnologyResponse> Get()
        {
            TechnologyResponse response = new TechnologyResponse();

            try
            {
                var itemsFound = _repository.GetTechnologies();
                response.TechnologyList = itemsFound;
                response.SuccessIndicator = true;
                logger.LogInformation(200,"SuccessIndicator = true");
                return Ok(response);
            }
            catch(Exception ex)
            {
                var faultObj = new Fault()
                {
                    FaultMessage = ex.Message,
                    FaultSource = ex.Source
                };

                if(ex.InnerException != null 
                    && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    faultObj.InnerMessage = ex.InnerException.Message;
                }

                response.Fault = faultObj;
                response.SuccessIndicator = false;
                logger.LogError(500,faultObj.FaultMessage);
                logger.LogInformation(500,"sucessIndicator = false");
                return new InternalServerErrorObjectResult(response);
            }
            
        }
        
        [HttpGet("{name}")]
        public ActionResult<IEnumerable<Technology>> Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }

            TechnologyResponse response = new TechnologyResponse();

            try
            {
                var technologyResponse = _repository.GetTechnologiesByName(name);
                if (technologyResponse == null)
                {
                    logger.LogInformation(200,"No Records Found");
                    return NotFound();
                }
                else
                {
                    response.TechnologyList = technologyResponse.ToList();
                    response.SuccessIndicator = true;
                    logger.LogInformation(200, "SuccessIndicator = true");
                    return Ok(response);
                }
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
            }

            return new InternalServerErrorObjectResult(response);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] Technology technology)
        {
            if(technology == null)
            {
                logger.LogInformation(400, "Bad Request");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                logger.LogInformation(422, "Validation error");
                return new UnprocessableEntityObjectResult(ModelState);
            }

            TechnologyResponse response = new TechnologyResponse()
            {
                TechnologyList = new List<Technology>()
                {
                    technology
                }
            };

            try
            {
                if(_repository.AddTechnology(technology) > 0)
                {
                    response.SuccessIndicator = true;
                    logger.LogInformation(200, "SuccessIndicator = true");
                    return Ok(response);
                }
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
            }

            return new InternalServerErrorObjectResult(response);
        }
        
        [HttpPut]
        public IActionResult Put([FromBody] Technology technology)
        {
            if (technology == null)
            {
                logger.LogInformation(400, "Bad Request");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                logger.LogInformation(422, "Validation error");
                return new UnprocessableEntityObjectResult(ModelState);
            }

            TechnologyResponse response = new TechnologyResponse()
            {
                TechnologyList = new List<Technology>()
                {
                    technology
                }
            };

            try
            {
                if (_repository.UpdateTechnology(technology) > 0)
                {
                    logger.LogInformation(200, "SuccessIndicator = true");
                    response.SuccessIndicator = true;
                    return Ok(response);
                }
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
            }

            return new InternalServerErrorObjectResult(response);
        }
        
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                logger.LogInformation(400, "Bad Request");
                return BadRequest();
            }

            TechnologyResponse response = new TechnologyResponse();

            try
            {
                if (_repository.DeleteTechnology(name) > 0)
                {
                    response.SuccessIndicator = true;
                    logger.LogInformation(200, "SuccessIndicator = true");
                    return Ok(response);
                }
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
            }

            return new InternalServerErrorObjectResult(response);
        }
    }
}
