
using Domain.Clients.Firebase.Models.RequestModels;
using Domain.Clients.Firebase.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Clients.Firebase.Models
{
    public interface IFirebaseClient
    {
        Task<FirebaseSignUpResponse> SignUpAsync(FirebaseSignUpRequest model);
        Task<FirebaseSignInResponse> SignInAsync(FirebaseSignUpRequest model);
        Task<PasswordResetResponse> PasswordResetAsync(PasswordResetRequest model);

        Task<VerifyEmailResponse> VerifyEmailAsync(VerifyEmailRequest model);
        Task<ChangeEmailResponse> ChangeEmailAsync(ChangeEmailRequest model);

        Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest model);

        /*Task<int> DeleteAsync(DeleteAccountRequest model);*/
    }
}
