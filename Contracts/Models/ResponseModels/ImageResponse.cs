using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.ResponseModels
{
    public class ImageResponse
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
    }
}
