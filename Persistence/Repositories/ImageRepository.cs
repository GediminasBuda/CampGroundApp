using Persistence.Models.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    class ImageRepository : IImageRepository
    {
        private readonly ISqlClient _sqlClient;
        private const string TableName = "images";
        public ImageRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }
        public Task<IEnumerable<ImageReadModel>> GetAllAsync(Guid campGroundId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE UserId = @UserId";
            return _sqlClient.QueryAsync<ImageReadModel>(sql, new
            {
                CampGroundId = campGroundId
            });
        }

       /* public Task<ImageReadModel> GetAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }*/

        public Task<int> SaveAsync(ImageReadModel model)
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
            throw new NotImplementedException();
        }
    }
}
