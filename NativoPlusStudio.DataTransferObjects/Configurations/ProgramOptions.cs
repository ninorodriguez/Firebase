using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NativoPlusStudio.DataTransferObjects.Configurations
{
    public class ProgramOptions
    {
        public decimal MaxAmountOfPercentageAllowed { get; set; }
        public decimal MaxAmountOfPercentageAllowedToCheckIfAnagram { get; set; }
        public string Environment { get; set; }       
    }
}
