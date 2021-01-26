using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Hahn.ApplicationProcess.December2020.Domain.Utilities
{
    public interface ICacheManagement
    {
        T CacheCall<T>(object key, int timeoutInSeconds, T procedureMethod);
        T GetValue<T>(object key, T defaultValue = default(T));
        void SetValue<T>(object key, T value, int timeoutInSeconds);
        void SetSlidingValue<T>(object key, T value, int timeoutInSeconds);
        void Remove(object key);
        void Remove(List<object> keys);
        void ClearAllCaches();

    }
}