using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MOD.AuthenticationService.Helpers;
using MOD.AuthenticationService.Models;
using MOD.AuthenticationService.Repositories;
using UnprocessableEntityObjectResult = MOD.AuthenticationService.Helpers.UnprocessableEntityObjectResult;

namespace MOD.AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository repository;
        private readonly ILogger<AuthenticationController> logger;
        private readonly IConfiguration configuration;

        public AuthenticationController(IAuthenticationRepository repository,
            ILogger<AuthenticationController> logger, IConfiguration configuration)
        {
            this.repository = repository;
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult UserLoginAsync([FromBody] User user)
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

            if (string.IsNullOrWhiteSpace(user.UserName) || 
                string.IsNullOrWhiteSpace(user.Password))
            {
                logger.LogInformation(400,"Username or Password not provided`");
                return new UnprocessableEntityObjectResult(ModelState);
            }

            AuthenticationResponse response = new AuthenticationResponse();

            try
            {
                if (repository.AuthenticateUser(user))
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
                    };

                    var signingKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                                configuration
                                    .GetSection("AuthenticationDetails")
                                    .GetSection("SecretKey").Value));

                    var token = new JwtSecurityToken(
                            issuer: configuration
                                        .GetSection("AuthenticationDetails")
                                        .GetSection("ValidIssuer").Value,
                            audience: configuration
                                        .GetSection("AuthenticationDetails")
                                        .GetSection("ValidAudience").Value,
                            expires: DateTime.Now.AddHours(1),
                            claims: claims,
                            signingCredentials:
                                new SigningCredentials(signingKey,
                                    SecurityAlgorithms.HmacSha256)
                        );

                    response.TokenList = new List<TokenResponse>()
                    {
                        new TokenResponse
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(
                                token),
                            Expiration = token.ValidTo
                        }
                    };

                    logger.LogInformation(200, "SuccessIndicator = true");
                    response.SuccessIndicator = true;

                    return Ok(response);
                }

                logger.LogInformation(200, 
                    "SuccessIndicator = false. User not authorized");
                return Unauthorized();
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
