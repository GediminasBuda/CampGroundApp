using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Contracts.Models.RequestModels
{
    public class SaveCommentRequest
    {
        [JsonPropertyName("campGroundId")]
        public Guid CampGroundId { get; set; }

        [JsonPropertyName("rating")]
        [Range(1, 5, ErrorMessage = "Rating range is between 1 and 5.")]
        public int Rating { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
