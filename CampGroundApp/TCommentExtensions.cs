using Contracts.Models.ResponseModels;
using Persistence.Models.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI
{
    public static class TCommentExtensions
    {
        public static CommentResponse MapToCommentResponse(this CommentReadModel model)
        {
            return new CommentResponse
            {
                Id = model.Id,
                CampGroundId = model.CampGroundId,
                Rating = model.Rating,
                Text = model.Text,
                UserId = model.UserId,
                DateCreated = model.DateCreated
            };
        }
    }
}
