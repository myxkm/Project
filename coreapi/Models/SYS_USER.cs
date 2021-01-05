using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreapi.Models
{
    public class SYS_USER
    {
        public int ID { get; set; }
        public string USERNAME { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public DateTime?  CreateTime { get; set; }
    }
}
