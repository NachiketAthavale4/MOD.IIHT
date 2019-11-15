using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOD.UserService.Helpers;
using MOD.UserService.Models;
using MOD.UserService.Repositories;
using UnprocessableEntityObjectResult = MOD.UserService.Helpers.UnprocessableEntityObjectResult;

namespace MOD.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserController> logger;

        public UserController(IUserRepository userRepository,
            ILogger<UserController> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            UserResponse response = new UserResponse();

            try
            {
                var allUsers = userRepository.GetUsers();
                response.SuccessIndicator = true;
                logger.LogInformation(200, "SuccessIndicator = true");
                response.UserList = allUsers;
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
        public ActionResult<User> Get(int id)
        {
            UserResponse response = new UserResponse();

            try
            {
                var userReturned = userRepository.GetUserById(id);
                if (userReturned != null)
                {
                    response.UserList = new List<User>() { userReturned };
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

        [HttpGet("[action]/{name}")]
        public ActionResult<User> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                logger.LogInformation(400, "Bad Request");
                return BadRequest();
            }

            UserResponse response = new UserResponse();

            try
            {
                var userReturned = userRepository.GetUserByName(name);
                if (userReturned != null)
                {
                    response.UserList = userReturned;
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
        public ActionResult Post([FromBody] User user)
        {
            if(user == null)
            {
                logger.LogInformation(400, "Bad Request");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                logger.LogInformation(422, "Validation error");
                return new UnprocessableEntityObjectResult(ModelState);
            }

            UserResponse response = new UserResponse()
            {
                UserList = new List<User>()
                {
                    user
                }
            };

            try
            {
                int operationSuccess = userRepository.SaveUser(user);
                if (operationSuccess > 0)
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
        public ActionResult Put([FromBody] User user)
        {
            if (user == null)
            {
                logger.LogInformation(400, "Bad Request");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                logger.LogInformation(422, "Validation error");
                return new UnprocessableEntityObjectResult(ModelState);
            }

            UserResponse response = new UserResponse()
            {
                UserList = new List<User>()
                {
                    user
                }
            };

            try
            {
                int operationSuccess = userRepository.UpdateUser(user);
                if (operationSuccess > 0)
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

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            UserResponse response = new UserResponse();

            try
            {
                if (userRepository.DeleteUserById(id))
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
