using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.RequestModels
{
    public class UpdateCampGroundRequest
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }
    }
}
