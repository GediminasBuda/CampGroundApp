using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestAPI.FirebaseSettings.Models
{
    public class User
    {
        [JsonPropertyName("login")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("returnsecuretoken")]
        public bool ReturnSecureToken { get; set; }

    }
}
