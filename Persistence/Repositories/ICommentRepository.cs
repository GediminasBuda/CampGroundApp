using Persistence.Models.ReadModels;
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

        Task<CommentReadModel> GetAsync(Guid id, Guid userId);

        Task<int> SaveOrUpdateAsync(CommentReadModel model);

        Task<int> DeleteAsync(Guid id);
    }
}
