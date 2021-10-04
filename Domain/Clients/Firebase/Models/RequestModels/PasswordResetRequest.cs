using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Clients.Firebase.Models.RequestModels
{
    public class PasswordResetRequest
    {
        [JsonPropertyName("requestType")]
        public string RequestType { get; } = "PASSWORD_RESET";
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
