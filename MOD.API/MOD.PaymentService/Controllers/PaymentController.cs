using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOD.PaymentService.Helpers;
using MOD.PaymentService.Models;
using MOD.PaymentService.Repositories;
using UnprocessableEntityObjectResult = MOD.PaymentService.Helpers.UnprocessableEntityObjectResult;

namespace MOD.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly ILogger<PaymentController> logger;

        public PaymentController(IPaymentRepository paymentRepository,
            ILogger<PaymentController> logger)
        {
            this.paymentRepository = paymentRepository;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Payment>> Get()
        {
            PaymentResponse response = new PaymentResponse();

            try
            {
                var itemsFound = paymentRepository.GetAllPayments();
                response.PaymentList = itemsFound;
                response.SuccessIndicator = true;
                logger.LogInformation(200, "SuccessIndicator = true");
                return Ok(response);
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

        [HttpGet("{id}")]
        public ActionResult<Payment> Get(int id)
        {
            PaymentResponse response = new PaymentResponse();

            try
            {
                var paymentItem = paymentRepository.GetPaymentById(id);
                if (paymentItem != null)
                {
                    response.PaymentList = new List<Payment>{ paymentItem };
                    response.SuccessIndicator = true;
                    logger.LogInformation(200, "SuccessIndicator = true");
                    return Ok(response);
                }
                logger.LogInformation(200, "No Records Found");
                return NotFound();
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
        public IActionResult Post([FromBody] Payment payment)
        {
            if(payment == null)
            {
                logger.LogInformation(400, "Bad Request");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                logger.LogInformation(422, "Validation error");
                return new UnprocessableEntityObjectResult(ModelState);
            }

            PaymentResponse response = new PaymentResponse()
            {
                PaymentList = new List<Payment>()
                {
                    payment
                }
            };

            try
            {
                if (paymentRepository.Save(payment) > 0)
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
        public IActionResult Put([FromBody] Payment payment)
        {
            if (payment == null)
            {
                logger.LogInformation(400, "Bad Request");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                logger.LogInformation(422, "Validation error");
                return new UnprocessableEntityObjectResult(ModelState);
            }

            PaymentResponse response = new PaymentResponse()
            {
                PaymentList = new List<Payment>()
                {
                    payment
                }
            };

            try
            {
                if (paymentRepository.Update(payment) > 0)
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

        [HttpGet("mentor/{mentorId}")]
        public IActionResult GetPaymentsByMentorId(int mentorId)
        {
            PaymentResponse response = new PaymentResponse();

            try
            {
                var paymentsFound = paymentRepository.GetPaymentsForMentor(mentorId);
                if (paymentsFound != null)
                {
                    response.SuccessIndicator = true;
                    logger.LogInformation(200, "SuccessIndicator = true");
                    response.PaymentList = paymentsFound;
                    return Ok(response);
                }
                logger.LogInformation(200, "No Records Found");
                return NotFound();
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

        [HttpGet("training/{trainingId}")]
        public IActionResult GetPaymentsByTrainingId(int trainingId)
        {
            PaymentResponse response = new PaymentResponse();

            try
            {
                var paymentsFound =
                    paymentRepository.GetPaymentsForTraining(trainingId);
                if (paymentsFound != null)
                {
                    response.SuccessIndicator = true;
                    logger.LogInformation(200, "SuccessIndicator = true");
                    response.PaymentList = paymentsFound;
                    return Ok(response);
                }
                logger.LogInformation(200, "No Records Found");
                return NotFound();
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            PaymentResponse response = new PaymentResponse();

            try
            {
                if (paymentRepository.Delete(id))
                {
                    response.SuccessIndicator = true;
                    logger.LogInformation(200, "SuccessIndicator = true");
                    return Ok(response);
                }
                logger.LogInformation(200, "No Records Found");
                return NotFound();
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
    }
}
