using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestAPI.FirebaseSettings.Models.ResponseModels
{
    public class ChangeEmailResponse
    {
        [JsonPropertyName("localId")]
        public string LocalId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("passwordHash")]
        public string PasswordHash { get; set; }

        [JsonPropertyName("idToken")]
        public string IdToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("expiresIn")]
        public string ExpiresIn { get; set; }

    }
}
