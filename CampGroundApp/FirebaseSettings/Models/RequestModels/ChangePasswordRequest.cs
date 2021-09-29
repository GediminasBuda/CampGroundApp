using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestAPI.FirebaseSettings.Models.RequestModels
{
    public class ChangePasswordRequest
    {
        [JsonPropertyName("idToken")]
        public string IdToken { get; set; }

        [JsonPropertyName("password")]
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [JsonPropertyName("returnSecureToken")]
        public bool ReturnSecureToken { get; set; }
    }
}
