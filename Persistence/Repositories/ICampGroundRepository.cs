using Persistence.Models.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
   public interface ICampGroundRepository
    {
        Task<IEnumerable<CampGroundReadModel>> GetAllAsync(Guid userId);

        Task<CampGroundReadModel> GetAsync(Guid id, Guid userId);

        Task<int> SaveOrUpdateAsync(CampGroundReadModel model);

        Task<int> DeleteAsync(Guid id);
    }
}
