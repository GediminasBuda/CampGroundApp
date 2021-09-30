using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence.Models.ReadModels;
using Domain.Clients.Firebase.Models.RequestModels;
using Contracts.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Persistence.Models.WriteModels;
using Domain.Clients.Firebase.Models;
using Domain.Clients.Firebase.Models.ResponseModels;

namespace RestAPI.Controllers
{
    [ApiController]
    /*[Route("auth")]*/
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IFirebaseClient _firebaseClient;

        public AuthController(IUserRepository userRepository, IFirebaseClient firebaseClient)
        {
            _userRepository = userRepository;
            _firebaseClient = firebaseClient;
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<ActionResult<SignUpResponse>> SignUp(FirebaseSignUpRequest request)
        {
            try
            {
                var user = await _firebaseClient.SignUpAsync(request);
                var userSql = new UserWriteModel

                {
                    UserId = Guid.NewGuid(),
                    Email = user.Email,
                    FirebaseId = user.FirebaseId

                };
                await _userRepository.SaveAsync(userSql);


                return Ok(new SignUpResponse
                {
                    UserId = userSql.UserId,
                    IdToken = user.IdToken

                });
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("signIn")]
        public async Task<ActionResult<FirebaseSignInResponse>> SignIn(FirebaseSignUpRequest request)
        {
            try
            {
                return await _firebaseClient.SignInAsync(request);
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpPost]
        [Route("resetPassword")]
        public async Task<ActionResult<PasswordResetResponse>> ResetPassword([FromBody] PasswordResetRequest request)
        {
            try
            {
                return await _firebaseClient.PasswordResetAsync(request);
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpPost]
        [Route("changeEmail")]
        public async Task<ActionResult<ChangeEmailResponse>> ChangeEmail([FromBody] ChangeEmailRequest request)
        {
            try
            {
                var userInfo = await _firebaseClient.ChangeEmailAsync(request);

                var verificationEmail = new VerifyEmailRequest
                {
                    IdToken = userInfo.IdToken
                };

                await _firebaseClient.VerifyEmailAsync(verificationEmail);

                return userInfo;
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpPost]
        [Route("changePassword")]
        public async Task<ActionResult<ChangePasswordResponse>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                return await _firebaseClient.ChangePasswordAsync(request);
            }
            catch (BadHttpRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
