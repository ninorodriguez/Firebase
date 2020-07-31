using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NativoPlusStudio.DataTransferObjects.Configurations
{
    public class FirebaseOptions
    {
        public string ApiKey { get; set; }
        public string ProjectId { get; set; }
        public string Bucket { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DBSecret { get; set; }
        public string BasePath { get; set; }
    }
}
