using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Clients.Firebase.Models.RequestModels
{
    public class VerifyEmailRequest
    {
        public string RequestType { get; } = "VERIFY_EMAIL";
        public string IdToken { get; set; }
    }
}
