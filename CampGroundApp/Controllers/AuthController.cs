using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence.Models.ReadModels;
using RestAPI.FirebaseSettings.Models.RequestModels;
using RestAPI.FirebaseSettings.Models.ResponseModels;
using Contracts.Models.ResponseModels;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("auth")]
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
            var user = await _firebaseClient.SignUpAsync(request);
            var userSql = new UserReadModel

            {
                UserId = Guid.NewGuid(),
                Email = user.Email,
                LocalId = user.LocalId
                
            };
            await _userRepository.SaveAsync(userSql);
            

            return Ok(new SignUpResponse
            {
                UserId = userSql.UserId,
                IdToken = user.IdToken
               
            });

        }

        [HttpPost]
        [Route("signIn")]
        public async Task<ActionResult<SignUpResponse>> SignIn(FirebaseSignUpRequest request)
        {
            var userName = await _firebaseClient.SignInAsync(request);
            return;
        }
    }

    /*public class SignUpRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }*/
}
