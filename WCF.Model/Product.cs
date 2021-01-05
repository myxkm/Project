using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace WCF.Model
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember(Name ="Code")]
        public string ProductCode { get; set; }
        [DataMember(Name ="Name")]
        public string ProductName { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
    }
}
