using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WCF.Model
{
    [DataContract]
    public class Department
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember(Name = "Code")]
        public string DepartmentCode { get; set; }
        [DataMember(Name = "Name")]
        public string DepartmentName { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
    }
}
