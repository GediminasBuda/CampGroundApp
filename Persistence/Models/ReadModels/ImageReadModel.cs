using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models.ReadModels
{
    public class ImageReadModel
    {
        public Guid Id { get; set; }
        public Guid CampGroundId { get; set; }
        public string Url { get; set; }
    }
}
