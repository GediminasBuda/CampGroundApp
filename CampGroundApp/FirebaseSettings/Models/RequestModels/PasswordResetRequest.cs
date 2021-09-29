using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.FirebaseSettings.Models.RequestModels
{
    public class PasswordResetRequest
    {
        public string RequestType { get; } = "PASSWORD_RESET";
        public string Email { get; set; }
    }
}
