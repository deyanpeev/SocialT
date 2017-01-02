using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialT.Common.Constants
{
    public class ValidationConstants
    {
        public const int MaxVmNameSize = 100;
        public const int MinVmNameSize = 5;
        public const double MaxVmMemory = 1048576;
        public const double MinVmMemory = 512;
        public const double MaxVmStorage = 102400;
        public const double MinVmStorage = 10;
        public const int MaxVmCores = 128;
        public const int MinVmCores = 1;
    }
}

