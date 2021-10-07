using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using Contracts.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Persistence.Models.WriteModels;
using Domain.Clients.Firebase.Models;
using Domain.Clients.Firebase.Models.ResponseModels;
using Contracts.Models.RequestModels;

namespace Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFirebaseClient _firebaseClient;

        public AuthService(IFirebaseClient firebaseClient, IUserRepository userRepository)
        {
            _firebaseClient = firebaseClient;
            _userRepository = userRepository;
        }
        public async Task<SignUpResponse> SignUpAsync(SignUpRequest request)
        {
                var user = await _firebaseClient.SignUpAsync(request.Email, request.Password);
                var userSql = new UserWriteModel

                {
                    UserId = Guid.NewGuid(),
                    Email = user.Email,
                    FirebaseId = user.FirebaseId,
                    DateCreated = DateTime.Now

                };
                await _userRepository.SaveAsync(userSql);

                return new SignUpResponse
                {
                    UserId = userSql.UserId,
                    IdToken = user.IdToken,
                    Email = userSql.Email,
                    DateCreated = userSql.DateCreated
                };
           
        }
        public async Task<SignInResponse> SignInAsync(SignInRequest request)
        {
            var firebaseSignInResponse = await _firebaseClient.SignInAsync(request.Email, request.Password);

            var user = await _userRepository.GetByIdAsync(firebaseSignInResponse.FirebaseId);

            return new SignInResponse
            {
                Email = user.Email,
                IdToken = firebaseSignInResponse.IdToken
            };
        }
    }
}
