using Contracts.Models.ResponseModels;
using Persistence.Models.ReadModels;


namespace RestAPI
{
    public static class TCommentExtensions
    {
        public static CommentResponse MapToCommentResponse(this CommentReadModel model)
        {
            return new CommentResponse
            {
                Id = model.Id,
                Rating = model.Rating,
                Text = model.Text,
                UserId = model.UserId,
                DateCreated = model.DateCreated
            };
        }
    }
}
