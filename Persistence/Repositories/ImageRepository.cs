using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ISqlClient _sqlClient;
        private const string TableName = "images";
        public ImageRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }
        public Task<IEnumerable<ImageReadModel>> GetByCampGroundIdAsync(Guid campGroundId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE CampGroundId = @CampGroundId";
            return _sqlClient.QueryAsync<ImageReadModel>(sql, new
            {
                CampGroundId = campGroundId
            });
        }
        public Task<ImageReadModel> GetAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id";

            return _sqlClient.QuerySingleOrDefaultAsync<ImageReadModel>(sql, new { Id = id });
        }
        public Task<int> SaveAsync(ImageWriteModel model)
        {
            var sql = @$"INSERT INTO {TableName} (Id, CampGroundId, Url) 
                        VALUES (@Id, @CampGroundId, @Url)";

            return _sqlClient.ExecuteAsync(sql, new
            {
                model.Id,
                model.CampGroundId,
                model.Url
            });
        }
        public Task<int> DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM {TableName} WHERE Id = @Id";

            return _sqlClient.ExecuteAsync(sql, new { Id = id });
        }

        public Task<int> DeleteByCampGroundIdAsync(Guid campGroundId)
        {
            var sql = $"DELETE FROM {TableName} WHERE CampgroundId = @CampgroundId";

            return _sqlClient.ExecuteAsync(sql, new { CampGroundId = campGroundId });
        }

        
    }
}
