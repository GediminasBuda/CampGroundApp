using Contracts.Models.RequestModels;
using Contracts.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence.Models.WriteModels;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICampGroundRepository _campGroundRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICampGroundRepository campGroundRepository, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _campGroundRepository = campGroundRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> ReadAll()
        {
            var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;
            var user = await _userRepository.GetByIdAsync(firebaseId);
            var comments = await _commentRepository.GetAllAsync(user.UserId);
            return new ActionResult<IEnumerable<CommentResponse>>(comments.Select(c => c.MapToCommentResponse()));
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SaveCommentResponse>> AddComment([FromBody] SaveCommentRequest request)//request comes from UI index.html
        {
            var campGround = await _campGroundRepository.GetAsync(request.CampGroundId);// we know which campGround is being commented;

            if (campGround is null)
            {
                return NotFound($"Campground with id: {request.CampGroundId} does not exist");
            }

            var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;//Very important!info comes from firebase.com. via API "https://localhost:5001;http://localhost:5000" connction; So we know which user is connected;

            var user = await _userRepository.GetByIdAsync(firebaseId);// we call user from our MySql Database;

            var comment = new CommentWriteModel
            {
                Id = Guid.NewGuid(),
                CampGroundId = request.CampGroundId,
                Rating = request.Rating,
                Text = request.Text,
                UserId = user.UserId,
                DateCreated = DateTime.Now
            };

            await _commentRepository.SaveOrUpdateAsync(comment);// we save comment into MySql Database;

            return new SaveCommentResponse // we return response to index.html
            {
                Id = comment.Id,
                CampGroundId = comment.CampGroundId,
                Rating = comment.Rating,
                Text = comment.Text,
                UserId = comment.UserId,
                DateCreated = comment.DateCreated
            };
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<UpdateCommentResponse>> UpdateComment(Guid id, [FromBody] UpdateCommentRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var ifExistsComment = await _commentRepository.GetAsync(id);

            if (ifExistsComment is null)
            {
                return NotFound($"Comment with id: {id} does not exist");
            }

            var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetByIdAsync(firebaseId);

            var commentToUpdate = await _commentRepository.GetAsync(id, user.UserId);

            if (commentToUpdate is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to edit information of this comment.");
            }
            var comment = new CommentWriteModel
            {
                Id = id,
                CampGroundId = commentToUpdate.CampGroundId,
                Rating = request.Rating,
                Text = request.Text,
                UserId = commentToUpdate.UserId,
                DateCreated = commentToUpdate.DateCreated
            };

            await _commentRepository.SaveOrUpdateAsync(comment);

            return new UpdateCommentResponse
            {
                Rating = comment.Rating,
                Text = comment.Text
            };
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult> DeleteComment(Guid id)
        {
            var ifExistsComment = await _commentRepository.GetAsync(id);

            if (ifExistsComment is null)
            {
                return NotFound($"Comment with id: {id} does not exist");
            }

            var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetByIdAsync(firebaseId);

            var commentToDelete = await _commentRepository.GetAsync(id, user.UserId);

            if (commentToDelete is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to delete this comment");
            }

            await _commentRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
