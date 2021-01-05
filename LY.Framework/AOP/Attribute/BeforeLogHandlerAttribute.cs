using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using LY.Framework.AOP.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.PolicyInjection.Policies;

namespace LY.Framework.AOP.Attribute
{
    public class BeforeLogHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new BeforeLogHandler() { Order = this.Order };
        }
    }
}
