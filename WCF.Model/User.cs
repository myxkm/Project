using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Model
{
    [DataContract]
    public class User
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string UserCode { get; set; }
        [DataMember]
        public DateTime JoinDate { get; set; }
        [DataMember]
        public DateTime LeaveDate { get; set; }

    }
}
