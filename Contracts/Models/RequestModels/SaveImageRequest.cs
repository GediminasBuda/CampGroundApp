using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Contracts.Models.RequestModels
{
    public class SaveImageRequest
    {
        [JsonPropertyName("campGroundId")]
        public Guid CampGroundId { get; set; }
        public string Url { get; set; }
    }
}
