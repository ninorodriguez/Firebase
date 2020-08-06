using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NativoPlusStudio.DataTransferObjects.Configurations
{
    public class DatadogOptions
    {
        public string ApiKey { get; set; }
        public string Source { get; set; }
        public string Service { get; set; }
        public string Tag { get; set; }
    }
}
