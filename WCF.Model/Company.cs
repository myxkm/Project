using System;
using System.Runtime.Serialization;

namespace WCF.Model
{

    [DataContract]
    public class Company
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime CreateTime { get; set; }


        public string PassWord { get; set; }
    }
}
