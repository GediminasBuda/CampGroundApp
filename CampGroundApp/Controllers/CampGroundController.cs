using Contracts.Models.RequestModels;
using Contracts.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models.WriteModels;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("campgrounds")]
    public class CampGroundController : ControllerBase
    {
        private readonly ICampGroundRepository _campGroundRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IImageRepository _imageRepository;

        public CampGroundController(ICampGroundRepository campGroundRepository, IUserRepository userRepository, ICommentRepository commentRepository, IImageRepository imageRepository)
        {
            _campGroundRepository = campGroundRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CampGroundsResponse>> GetCampgrounds()
        {
            var campGrounds = await _campGroundRepository.GetAllAsync();

            return campGrounds.Select(campGround => new CampGroundsResponse
            {
                Id = campGround.Id,
                Name = campGround.Name,
                Price = campGround.Price,
                Description = campGround.Description
                
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CampGroundResponse>> GetCampGround(Guid id)
        {
            var campGround = await _campGroundRepository.GetAsync(id);

            if (campGround is null)
            {
                return NotFound($"CampGround with id: {id} does not exist");
            }

            var commentsToResponse = await _commentRepository.GetByCampGroundIdAsync(id);

            var comments = commentsToResponse.Select(comment => new CommentResponse
            {
                Id = comment.Id,
                CampGroundId = comment.CampGroundId,
                Rating = comment.Rating,
                Text = comment.Text,
                UserId = comment.UserId,
                DateCreated = comment.DateCreated
            });

            var commentsList = comments.ToList();

            var imagesToResponse = await _imageRepository.GetByCampGroundIdAsync(id);

            var images = imagesToResponse.Select(image => new ImageResponse
            {
                Id = image.Id,
                Url = image.Url
            });

            var imagesList = images.ToList();

            return new CampGroundResponse
            {
                Id = campGround.Id,
                Name = campGround.Name,
                Price = campGround.Price,
                Description = campGround.Description,
                Images = imagesList,
                Comments = commentsList
            };
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SaveCampGroundResponse>> AddCampGround([FromBody] SaveCampGroundRequest request)
        {
            var localId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetByIdAsync(localId);

            var campGround = new CampGroundWriteModel
            {
                Id = Guid.NewGuid(),
                UserId = user.UserId,
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                DateCreated = DateTime.Now
            };

            await _campGroundRepository.SaveOrUpdateAsync(campGround);

            return CreatedAtAction(nameof(GetCampGround), new { id = campGround.Id }, new SaveCampGroundResponse
            {
                Id = campGround.Id,
                UserId = campGround.UserId,
                Name = campGround.Name,
                Price = campGround.Price,
                Description = campGround.Description,
                DateCreated = campGround.DateCreated
            });
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<UpdateCampGroundResponse>> UpdateCampGround(Guid id, [FromBody] UpdateCampGroundRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var ifExistsCampGround = await _campGroundRepository.GetAsync(id);

            if (ifExistsCampGround is null)
            {
                return NotFound($"CampGround with id: {id} does not exist");
            }

            var localId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetByIdAsync(localId);

            var campGroundToUpdate = await _campGroundRepository.GetAsync(id, user.UserId);

            if (campGroundToUpdate is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to edit information of this CampGround");
            }

            var campGround = new CampGroundWriteModel
            {
                Id = id,
                UserId = campGroundToUpdate.UserId,
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                DateCreated = campGroundToUpdate.DateCreated
            };

            await _campGroundRepository.SaveOrUpdateAsync(campGround);

            return CreatedAtAction(nameof(GetCampGround), new { id = campGround.Id }, new UpdateCampGroundResponse
            {
                Name = campGround.Name,
                Price = campGround.Price,
                Description = campGround.Description,
            });
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult> DeleteCampGround(Guid id)
        {
            var ifExistsCampground = await _campGroundRepository.GetAsync(id);

            if (ifExistsCampground is null)
            {
                return NotFound($"Campground with id: {id} does not exist");
            }

            var localId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetByIdAsync(localId);

            var campgroundToDelete = await _campGroundRepository.GetAsync(id, user.UserId);

            if (campgroundToDelete is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to delete this CampGround");
            }

            var taskToDeleteComments = _commentRepository.DeleteByCampGroundIdAsync(id);
            var taskToDeleteImages = _imageRepository.DeleteByCampGroundIdAsync(id);
            var taskToDeleteCampGround = _campGroundRepository.DeleteAsync(id);

            await Task.WhenAll(taskToDeleteComments, taskToDeleteImages, taskToDeleteCampGround);

            return NoContent();
        }

    }
}
