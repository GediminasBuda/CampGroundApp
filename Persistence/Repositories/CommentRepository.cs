using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ISqlClient _sqlClient;
        private const string TableName = "comments";
        public CommentRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<IEnumerable<CommentReadModel>> GetAllAsync(Guid userId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE UserId = @UserId";

            return _sqlClient.QueryAsync<CommentReadModel>(sql, new
            {
                UserId = userId
            });
        }
        public Task<IEnumerable<CommentReadModel>> GetByCampGroundIdAsync(Guid campGroundId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE CampGroundId = @CampGroundId";

            return _sqlClient.QueryAsync<CommentReadModel>(sql, new
            {
                CampGroundId = campGroundId
            });
        }
        public Task<CommentReadModel> GetAsync(Guid id)
        {
            var sql = $"SELECT FROM {TableName} WHERE Id = @Id";

            return _sqlClient.QuerySingleOrDefaultAsync<CommentReadModel>(sql, new
            {
                Id = id
            });
        }
        public Task<CommentReadModel> GetAsync(Guid id, Guid userId)
        {
            var sql = $"SELECT FROM {TableName} WHERE Id = @Id AND UserId = @UserId";

            return _sqlClient.QuerySingleOrDefaultAsync<CommentReadModel>(sql, new
            {
                Id = id,
                UserId = userId
            });
        }

        public Task<int> SaveOrUpdateAsync(CommentWriteModel model)
        {
            var sql = @$"INSERT INTO {TableName} (Id, CampGroundId, Rating, Text, UserId, DateCreated) 
                        VALUES (@Id, @CampGroundId, @Rating, @Text, @UserId, @DateCreated)
                        ON DUPLICATE KEY UPDATE Rating = @Rating, Text = @Text";

            return _sqlClient.ExecuteAsync(sql, new
            {
                model.Id,
                model.CampGroundId,
                model.Rating,
                model.Text,
                model.UserId,
                model.DateCreated
            });
        }
        public Task<int> DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM {TableName} WHERE Id = @Id";

            return _sqlClient.ExecuteAsync(sql, new
            {
                Id = id
            });
        }

        public Task<int> DeleteByCampGroundIdAsync(Guid campGroundId)
        {
            var sql = $"DELETE FROM {TableName} WHERE CampGroundId = @CampGroundId";

            return _sqlClient.ExecuteAsync(sql, new
            {
                CampGroundId = campGroundId
            });
        }

    }
}
