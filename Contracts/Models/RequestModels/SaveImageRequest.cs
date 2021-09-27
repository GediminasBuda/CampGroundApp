using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.RequestModels
{
    public class SaveImageRequest
    {
        public Guid CampGroundId { get; set; }
        public string Url { get; set; }
    }
}
