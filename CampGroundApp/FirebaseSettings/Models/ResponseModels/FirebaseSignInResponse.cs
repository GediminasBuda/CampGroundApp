using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.FirebaseSettings.Models.ResponseModels
{
    public class FirebaseSignInResponse
    {
        public string IdToken { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }
        public string FirebaseId { get; set; }
        public bool Registered { get; set; }

    }
}
