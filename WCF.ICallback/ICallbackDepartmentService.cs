﻿using System.ServiceModel;

namespace WCF.ICallback
{
    public interface ICallbackDepartmentService
    {
        [OperationContract(IsOneWay = true)]
        void Result(long m, long n, long r);
    }
}