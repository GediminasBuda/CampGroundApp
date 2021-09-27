using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.ResponseModels
{
    public class SaveImageResponse
    {
        public Guid Id { get; set; }

        public Guid CampGroundId { get; set; }

        public string Url { get; set; }
    }
}
