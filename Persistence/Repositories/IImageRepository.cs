using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageReadModel>> GetByCampGroundIdAsync(Guid campGroundId);

        Task<ImageReadModel> GetAsync(Guid id);

        Task<int> SaveAsync(ImageWriteModel model);

        Task<int> DeleteAsync(Guid id);

        Task<int> DeleteByCampGroundIdAsync(Guid campGroundId);
    }
}
