using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.ResponseModels
{
    public class UpdateCommentResponse
    {
        public int Rating { get; set; }

        public string Text { get; set; }
    }
}
