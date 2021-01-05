using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreapi.Models
{
    public class AuthorizationCenter
    {
    }
    public class RestfulData
    {
        /// <summary>
        /// <![CDATA[错误码]]>
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        ///<![CDATA[消息]]>
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// <![CDATA[相关的链接帮助地址]]>
        /// </summary>
        public string Url { get; set; }

    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RestfulData<T> : RestfulData
    {
        /// <summary>
        /// <![CDATA[数据]]>
        /// </summary>
        public virtual T Data { get; set; }
    }

    /// <summary>
    /// <![CDATA[返回数组]]>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RestfulArray<T> : RestfulData<IEnumerable<T>>
    {

    }
    public class TokenObj
    {
        /// <summary>
        /// token内容
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// //过期时间
        /// </summary>
        public long Expires { get; set; }
    }

}
