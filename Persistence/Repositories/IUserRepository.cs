using Persistence.Models.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<UserReadModel> GetByIdAsync(string userId);
        Task<int> SaveAsync(UserReadModel model);
    }
}
