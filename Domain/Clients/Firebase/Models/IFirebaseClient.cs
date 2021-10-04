
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
        Task<FirebaseSignUpResponse> SignUpAsync(string email, string password);
        Task<FirebaseSignInResponse> SignInAsync(string email, string password);
        Task<PasswordResetResponse> PasswordResetAsync(PasswordResetRequest model);

        Task<VerifyEmailResponse> VerifyEmailAsync(VerifyEmailRequest model);
        Task<ChangeEmailResponse> ChangeEmailAsync(ChangeEmailRequest model);

        Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest model);

        /*Task<int> DeleteAsync(DeleteAccountRequest model);*/
    }
}
