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

namespace RestApi.Controllers
{
    [ApiController]
    [Route("images")]
    public class ImageController : ControllerBase
    {
        private readonly ICampGroundRepository _campGroundRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;

        public ImageController(ICampGroundRepository campGroundRepository, IUserRepository userRepository, IImageRepository imageRepository)
        {
            _campGroundRepository = campGroundRepository;
            _userRepository = userRepository;
            _imageRepository = imageRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SaveImageResponse>> AddImage([FromBody] SaveImageRequest request)
        {
            var campground = await _campGroundRepository.GetAsync(request.CampGroundId);

            if (campground is null)
            {
                return NotFound($"CampGround with id: {request.CampGroundId} does not exist");
            }

            var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;//user session code routine;

            var user = await _userRepository.GetByIdAsync(firebaseId);//user session code routine;

            var campGroundToAddImage = await _campGroundRepository.GetAsync(request.CampGroundId, user.UserId);// checks if CampGround with the given UserId exists;

            if (campGroundToAddImage is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to add the image to this CampGround");
            }

            var image = new ImageWriteModel
            {
                Id = Guid.NewGuid(),
                CampGroundId = request.CampGroundId,
                Url = request.Url
            };

            await _imageRepository.SaveAsync(image);

            return new SaveImageResponse
            {
                Id = image.Id,
                CampGroundId = image.CampGroundId,
                Url = image.Url
            };
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult> DeleteImage(Guid id)
        {
            var ifExistsImage = await _imageRepository.GetAsync(id);

            if (ifExistsImage is null)
            {
                return NotFound($"Image with id: {id} does not exist");
            }

            var firebaseId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetByIdAsync(firebaseId);

            var campground = await _campGroundRepository.GetAsync(ifExistsImage.CampGroundId, user.UserId);

            if (campground is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to delete image of this CampGround");
            }

            await _imageRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
/*[HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<ActionResult<UpdateImageResponseModel>> UpdateImage(Guid id, [FromBody] UpdateImageRequestModel request)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var ifExistsImage = await _imageRepository.GetAsync(id);

            if (ifExistsImage is null)
            {
                return NotFound($"Comment with id: {id} does not exist");
            }

            var localId = HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == "user_id").Value;

            var user = await _userRepository.GetAsync(localId);

            var campground = await _campgroundRepository.GetAsync(ifExistsImage.CampgroundId, user.Id);

            if (campground is null)
            {
                return BadRequest($"The user with e-mail: {user.Email} does not have permission to edit image of this campground");
            }

            var image = new ImageWriteModel
            {
                Id = id,
                CampgroundId = campground.Id,
                Url = request.Url
            };

            await _imageRepository.SaveOrUpdateAsync(image);

            return new UpdateImageResponseModel
            {
                Url = image.Url
            };
        }*/