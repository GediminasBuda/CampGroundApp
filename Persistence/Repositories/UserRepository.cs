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

        public Task<UserReadModel> GetByIdAsync(string firebaseId)
        {
            var sql = $"SELECT * FROM {TableName} WHERE FirebaseId = @FirebaseId";
            return _sqlClient.QuerySingleOrDefaultAsync<UserReadModel>(sql, new
            {
                FirebaseId = firebaseId
            });

        }

        public Task<int> SaveAsync(UserWriteModel model)
        {
            var sql = $"INSERT INTO {TableName} (UserId, Email, FirebaseId) VALUES (@UserId, @Email, @FirebaseId)";
            return _sqlClient.ExecuteAsync(sql, model);
        }
    }
}

