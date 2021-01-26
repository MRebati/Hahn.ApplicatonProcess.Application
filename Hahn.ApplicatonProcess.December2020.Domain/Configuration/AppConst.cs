using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicationProcess.December2020.Domain.Configuration
{
    public static class AppConst
    {
        public static TimeSpan TransactionTimeout = TimeSpan.FromMinutes(1);

        public static class ExternalApis
        {
            public static class CountryCheck
            {
                public static string Url = "External:CountryCheck:Url";
            }
        }

        public class Cache
        {
            public static int DefaultTimeoutInSeconds = 360;
            public static string CountryList = "country-list";
        }
    }
}
