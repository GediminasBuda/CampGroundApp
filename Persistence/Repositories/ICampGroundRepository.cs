using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
   public interface ICampGroundRepository
    {
        Task<IEnumerable<CampGroundReadModel>> GetAllAsync();

        Task<CampGroundReadModel> GetAsync(Guid id);

        Task<CampGroundReadModel> GetAsync(Guid id, Guid userId);

        Task<int> SaveOrUpdateAsync(CampGroundWriteModel model);

        Task<int> DeleteAsync(Guid id);
    }
}
