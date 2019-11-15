using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOD.SearchService.Helpers;
using MOD.SearchService.Models;
using MOD.SearchService.Repositories;

namespace MOD.SearchService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchRepository searchRepository;
        private readonly ILogger<SearchController> logger;

        public SearchController(ISearchRepository searchRepository,
            ILogger<SearchController> logger)
        {
            this.searchRepository = searchRepository;
            this.logger = logger;
        }

        [HttpGet("{technology}")]
        public ActionResult<IEnumerable<SearchResponse>> Get(string technology)
        {
            if (string.IsNullOrWhiteSpace(technology))
            {
                logger.LogInformation(400,
                    "Bad Request. Technology string empty or null.");
                return BadRequest();
            }

            SearchResponse response = new SearchResponse();

            try
            {
                var itemsFound =
                    searchRepository.GetTrainingsByTechnology(technology);
                if (itemsFound != null)
                {
                    logger.LogInformation(200, "SuccessIndicator = true");
                    response.SuccessIndicator = true;
                    response.TrainingsList = itemsFound;
                    return Ok(itemsFound);
                }
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
