using Persistence.Models.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CampGroundRepository : ICampGroundRepository
    {
        private const string TableName = "campground";
        private readonly ISqlClient _sqlClient;

        public CampGroundRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }
       
        public Task<IEnumerable<CampGroundReadModel>> GetAllAsync(Guid userId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE UserId = @UserId";

            return _sqlClient.QueryAsync<CampGroundReadModel>(sql, new
            {
                UserId = userId
            });
        }

        public Task<CampGroundReadModel> GetAsync(Guid id, Guid userId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id AND UserId = @UserId";

            return _sqlClient.QuerySingleOrDefaultAsync<CampGroundReadModel>(sql, new { Id = id, UserId = userId });
        }

        public Task<int> SaveOrUpdateAsync(CampGroundReadModel model)
        {
            var sql = @$"INSERT INTO {TableName} (Id, UserId, Name, Price, Description, DateCreated) 
                        VALUES (@Id, @UserId, @Name, @Price, @Description, @DateCreated)
                        ON DUPLICATE KEY UPDATE Name = @Name, Price = @Price, Description = @Description";

            return _sqlClient.ExecuteAsync(sql, new
            {
                model.Id,
                model.UserId,
                model.Name,
                model.Price,
                model.Description,
                model.DateCreated
            });
        }
        public Task<int> DeleteAsync(Guid id)
        {
            var sql = $"DELETE from {TableName} WHERE Id = @Id";
            return _sqlClient.ExecuteAsync(sql, new { Id = id });
        }

    }
}
