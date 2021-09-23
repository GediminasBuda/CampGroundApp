using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAPI.FirebaseSettings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CommentController> _logger;

        public CommentController(ILogger<CommentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Comment> Get()
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var userId2 = (Guid)HttpContext.Items["userId"];

            // if (userId is not null)
            // {
            //     return NotFound("");
            // }
            //
            // var id = userId.Value;
            //
            // var userModel = _usersRepository.Get(id);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Comment
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
                .ToArray();
        }
    }
}
