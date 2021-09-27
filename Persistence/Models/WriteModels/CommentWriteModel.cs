using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models.WriteModels
{
    public class CommentWriteModel
    {
        public Guid Id { get; set; }

        public Guid CampGroundId { get; set; }

        public int Rating { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
