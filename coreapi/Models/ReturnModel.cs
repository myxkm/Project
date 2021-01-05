using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreapi.Models
{
    public class ReturnModel
    {
        public string Message { get; set; }
        public bool ActionOK { get; set; } = true;
        public dynamic Data { get; set; } = null;
        public string Url { get; set; } = string.Empty;
    }
}
