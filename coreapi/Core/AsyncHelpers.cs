using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace coreapi.Core
{
    internal static class AsyncHelpers
    {
        internal static int GetManagedThreadId()
        {
#if NETSTANDARD1_3
            return System.Environment.CurrentManagedThreadId;
#else
            return Thread.CurrentThread.ManagedThreadId;
#endif
        }

        internal static void StartAsyncTask(Action<object> action, object state)
        {
#if NET4_0 || NET4_5 || NETSTANDARD
            System.Threading.Tasks.Task.Factory.StartNew(action, state, CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default);
#else
            ThreadPool.QueueUserWorkItem(new WaitCallback(action), state);
#endif
        }
    }
}
