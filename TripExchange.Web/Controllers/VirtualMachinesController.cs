namespace SocialUnion.Controllers
{
    using Models;
    using Models.VirtualMachines;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Common.Constants;
    using SocialUnion.Web.Controllers;
    using SocialUnion.Data;

    public class VirtualMachinesController : BaseApiController
    {
       public VirtualMachinesController()
            : this(new SocialUnionData())
        {
        }

       public VirtualMachinesController(ISocialUnionData data)
            : base(data)
        {
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            var result = this.Data.VirtualMachines.All()
                .OrderBy(vm => vm.Name)
                .Take(10)
                .Select(VirtualMachinePublicViewModel.FromVirtualMachine);

            return this.Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("api/VirtualMachines/ChangeActiveState")]
        public IHttpActionResult ChangeActiveState(Guid id, bool isOn)
        {
            VirtualMachine vm = this.Data.VirtualMachines.GetById(id);
            if (vm == null)
            {
                return BadRequest("A virtual machine with that id doesn't exist.");
            }

            vm.IsOn = isOn;
            this.Data.VirtualMachines.SaveChanges();

            return this.Ok(vm.IsOn);
        }

        [Authorize]
        [HttpPost]
        [Route("api/VirtualMachines/ChangeParamethers")]
        public IHttpActionResult ChangeParameters(VirtualMachineRequestModifyModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest("The request model is not valid.");
            }

            VirtualMachine vm = null;
            vm = this.Data.VirtualMachines.All().Single(v => v.Name == model.Name);

            if (model.Memory >= ValidationConstants.MinVmMemory)
                vm.Memory = model.Memory;

            if (model.Storage >= ValidationConstants.MinVmStorage)
                vm.Storage = model.Storage;

            if (model.CpuCores >= ValidationConstants.MinVmCores)
                vm.CpuCores = model.CpuCores;

            return Ok("Virtual machine was successfully modified.");
        }

        [Authorize]
        [HttpPost]
        [Route("api/VirtualMachines/AddVirtualMachine")]
        public IHttpActionResult AddVirtualMachine(VirtualMachineRequestCreateModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest("The request model is not valid.");
            }

            var newVirtualMachine = new VirtualMachine
            {
                Name = model.Name,
                CpuCores = model.CpuCores,
                Memory = model.Memory,
                Storage = model.Storage,
                IsOn = false
            };

            this.Data.VirtualMachines.Add(newVirtualMachine);
            this.Data.VirtualMachines.SaveChanges();

            return Ok(newVirtualMachine.Id);
        }
    }
}
