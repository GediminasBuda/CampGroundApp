using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("signUp")]
        public IActionResult SignUp(SignUpRequest request)
        {
            // call firebase
            // save to persistence

            // new
            // {
            //     UserId = Guid.NewGuid(),
            //     FirebaseId = Response.Id,
            //     SebUserId = 
            // }

            return Ok(new
            {
                UserId = Guid.NewGuid(),
                IdToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6ImFlNTJiOGQ4NTk4N2U1OWRjYWM2MmJlNzg2YzcwZTAyMDcxN2I0MTEiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vY2FtcGdyb3VuZGFwcC00MDhmZSIsImF1ZCI6ImNhbXBncm91bmRhcHAtNDA4ZmUiLCJhdXRoX3RpbWUiOjE2MzI0MTk0NjcsInVzZXJfaWQiOiJ0WVJqOUJSQzhkY3VYRzlrQm91b1NCSWJhQWoyIiwic3ViIjoidFlSajlCUkM4ZGN1WEc5a0JvdW9TQkliYUFqMiIsImlhdCI6MTYzMjQxOTQ2NywiZXhwIjoxNjMyNDIzMDY3LCJlbWFpbCI6InRlc3RhczJAdGVzdGFzLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6eyJlbWFpbCI6WyJ0ZXN0YXMyQHRlc3Rhcy5jb20iXX0sInNpZ25faW5fcHJvdmlkZXIiOiJwYXNzd29yZCJ9fQ.D_f2aFp6b_RMR4Pfy1tUdACvtJD-kRxjuKnha7i-QaS7Z58ZROJ3w6cXEAtkRhHCzuyn7rfei51jbO9FXhlEj1CkxLKFN17O3cO5id-Zyn-vdTfZY-QzQFJNS2oxd1dbVZF7UPF0UEmN4RfYuj9gfFGZ4qkWMb0ETU52bXsR6EkPS_zxjAOp0K0SwVUmpZ8468Eg6f1gLjNQ3oBxahiA4a-qSieKYsy9xlyLmXAIv1NJLLczMYkEyFUM4GhFN4EyTpjDp-037K6Ghby69Py8cwFMtv5fXlMJ1YIRtMpdeHkGgfhOWjDZQ072QOjDXG1trIO7nm_ua_eYeJC3jA2ORQ"
            });
        }

        [HttpPost]
        [Route("signIn")]
        public IActionResult SignIn(SignUpRequest request)
        {
            return Ok(new
            {
                UserId = Guid.NewGuid(),
                IdToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6ImFlNTJiOGQ4NTk4N2U1OWRjYWM2MmJlNzg2YzcwZTAyMDcxN2I0MTEiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vY2FtcGdyb3VuZGFwcC00MDhmZSIsImF1ZCI6ImNhbXBncm91bmRhcHAtNDA4ZmUiLCJhdXRoX3RpbWUiOjE2MzI0MTk0NjcsInVzZXJfaWQiOiJ0WVJqOUJSQzhkY3VYRzlrQm91b1NCSWJhQWoyIiwic3ViIjoidFlSajlCUkM4ZGN1WEc5a0JvdW9TQkliYUFqMiIsImlhdCI6MTYzMjQxOTQ2NywiZXhwIjoxNjMyNDIzMDY3LCJlbWFpbCI6InRlc3RhczJAdGVzdGFzLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6eyJlbWFpbCI6WyJ0ZXN0YXMyQHRlc3Rhcy5jb20iXX0sInNpZ25faW5fcHJvdmlkZXIiOiJwYXNzd29yZCJ9fQ.D_f2aFp6b_RMR4Pfy1tUdACvtJD-kRxjuKnha7i-QaS7Z58ZROJ3w6cXEAtkRhHCzuyn7rfei51jbO9FXhlEj1CkxLKFN17O3cO5id-Zyn-vdTfZY-QzQFJNS2oxd1dbVZF7UPF0UEmN4RfYuj9gfFGZ4qkWMb0ETU52bXsR6EkPS_zxjAOp0K0SwVUmpZ8468Eg6f1gLjNQ3oBxahiA4a-qSieKYsy9xlyLmXAIv1NJLLczMYkEyFUM4GhFN4EyTpjDp-037K6Ghby69Py8cwFMtv5fXlMJ1YIRtMpdeHkGgfhOWjDZQ072QOjDXG1trIO7nm_ua_eYeJC3jA2ORQ"
            });
        }
    }

    public class SignUpRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
