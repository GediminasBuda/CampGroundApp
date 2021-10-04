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
using Domain.Services;
using Contracts.Models.RequestModels;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IFirebaseClient _firebaseClient;
        private readonly IAuthService _authService;

        public AuthController(IUserRepository userRepository, IFirebaseClient firebaseClient, IAuthService authService)
        {
            _userRepository = userRepository;
            _firebaseClient = firebaseClient;
            _authService = authService;
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<ActionResult<SignUpResponse>> SignUp(SignUpRequest request)
        {
            try
            {
                var response = await _authService.SignUpAsync(request);

                return response;
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("signIn")]
        public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest request)
        {
            try
            {
                return await _authService.SignInAsync(request);
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
