using Persistence.Models.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    interface IImageRepository
    {
        Task<IEnumerable<ImageReadModel>> GetAllAsync(Guid campGroundId);

        //Task<ImageReadModel> GetAsync(Guid id, Guid userId);

        Task<int> SaveAsync(ImageReadModel model);

        Task<int> DeleteAsync(Guid id);
    }
}
