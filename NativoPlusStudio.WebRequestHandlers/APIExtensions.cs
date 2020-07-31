using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NativoPlusStudio.WebRequestHandlers
{
    public static class APIExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string myString)
        {
            return string.IsNullOrEmpty(myString) || string.IsNullOrWhiteSpace(myString);
        }
    }
}
