using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialUnion.Common.Constants;

namespace SocialUnion.Models.VirtualMachines
{
    public class VirtualMachineRequestModifyModel
    {
        [Required]
        [MinLength(ValidationConstants.MinVmNameSize)]
        [MaxLength(ValidationConstants.MaxVmNameSize)]
        public string Name { get; set; }
        
        [Range(ValidationConstants.MinVmCores, ValidationConstants.MaxVmCores)]
        public int CpuCores { get; set; }
        
        [Range(ValidationConstants.MinVmMemory, ValidationConstants.MaxVmMemory)]
        public double Memory { get; set; }
        
        [Range(ValidationConstants.MinVmStorage, ValidationConstants.MaxVmStorage)]
        public double Storage { get; set; }
    }
}
