using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SocialUnion.Models;

namespace SocialUnion.Models.VirtualMachines
{
    public class VirtualMachinePublicViewModel
    {
        public static Expression<Func<VirtualMachine, VirtualMachinePublicViewModel>> FromVirtualMachine
        {
            get
            {
                return a => new VirtualMachinePublicViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    IsOn = a.IsOn,
                    Memory = a.Memory,
                    Storage = a.Storage,
                    CpuCores = a.CpuCores
                };
            }
        }

        public Guid Id { get; set; }    

        public string Name { get; set; }

        public bool IsOn { get; set; }

        public int CpuCores { get; set; }

        public double Memory { get; set; }

        public double Storage { get; set; }
    }
}