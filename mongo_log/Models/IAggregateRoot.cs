using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBUtils.Models
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public interface IAggregateRoot
    {
        string Id { set; get; }
    }
}