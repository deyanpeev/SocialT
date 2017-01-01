using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialUnion.Common.Constants;

namespace SocialUnion.Models
{
    public class VirtualMachine
    {
        public VirtualMachine()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinVmNameSize)]
        [MaxLength(ValidationConstants.MaxVmNameSize)]
        public string Name { get; set; }

        public bool IsOn { get; set; }

        [Required]
        [Range(ValidationConstants.MinVmCores, ValidationConstants.MaxVmCores)]
        public int CpuCores { get; set; }

        [Required]
        [Range(ValidationConstants.MinVmMemory, ValidationConstants.MaxVmMemory)]
        public double Memory { get; set; }

        [Required]
        [Range(ValidationConstants.MinVmStorage, ValidationConstants.MaxVmStorage)]
        public double Storage { get; set; }
    }
}
