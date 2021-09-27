using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.ResponseModels
{
    public class SignUpResponse
    {
        public Guid UserId { get; set; }
        public string IdToken { get; set; }
    }
}
