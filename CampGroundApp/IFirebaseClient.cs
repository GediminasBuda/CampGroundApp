using RestAPI.FirebaseSettings.Models;
using RestAPI.FirebaseSettings.Models.RequestModels;
using RestAPI.FirebaseSettings.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI
{
    interface IFirebaseClient
    {
        Task<SignUpResponse> SignUpAsync(SignUpRequest signUp);
        Task<SignInResponse> SignInAsync(SignUpRequest signIn);

        Task<T> EditFirebaseUser();

        
    }
}
