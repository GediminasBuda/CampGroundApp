using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
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
        private const string TableNameImage = "images";
        private readonly ISqlClient _sqlClient;

        public CampGroundRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }
       
        public Task<IEnumerable<CampGroundReadModel>> GetAllAsync()
        {
            var sql = $"SELECT {TableName}.Id, {TableName}.UserId, {TableName}.Name, {TableName}.Price, {TableName}.Description, {TableName}.DateCreated, {TableNameImage}.Url " +
                $"FROM {TableName} left join {TableNameImage} ON {TableName}.Id = {TableNameImage}.CampGroundId " +
                $"GROUP BY {TableName}.Id";

            return _sqlClient.QueryAsync<CampGroundReadModel>(sql);
        }
        public Task<CampGroundReadModel> GetAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id";

            return _sqlClient.QuerySingleOrDefaultAsync<CampGroundReadModel>(sql, new
            {
                Id = id
            });
        }
        public Task<CampGroundReadModel> GetAsync(Guid id, Guid userId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id AND UserId = @UserId";

            return _sqlClient.QuerySingleOrDefaultAsync<CampGroundReadModel>(sql, new { Id = id, UserId = userId });
        }

        public Task<int> SaveOrUpdateAsync(CampGroundWriteModel model)
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
