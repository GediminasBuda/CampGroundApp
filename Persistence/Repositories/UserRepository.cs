using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ISqlClient _sqlClient;
        private const string TableName = "users";

        public UserRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public Task<UserReadModel> GetByIdAsync(string localId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE LocalId = @LocalId";
            return _sqlClient.QuerySingleOrDefaultAsync<UserReadModel>(sql, new
            {
                LocalId = localId
            });

        }

        public Task<int> SaveAsync(UserWriteModel model)
        {
            var sql = $"INSERT INTO {TableName} (UserId, Email, LocalId) VALUES (@UserId, @Email, @LocalId)";
            return _sqlClient.ExecuteAsync(sql, model);
        }
    }
}

