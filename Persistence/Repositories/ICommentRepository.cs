using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentReadModel>> GetAllAsync(Guid userId);

        Task<IEnumerable<CommentReadModel>> GetByCampGroundIdAsync(Guid campGroundId);

        Task<CommentReadModel> GetAsync(Guid id);

        Task<CommentReadModel> GetAsync(Guid id, Guid userId);

        Task<int> SaveOrUpdateAsync(CommentWriteModel model);

        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteByCampGroundIdAsync(Guid campGroundId);
    }
}
