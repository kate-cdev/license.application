using license.application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace license.application.Dto
{
    public class MachineDto
    {
        public MachineDto()
        {
        }
      

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public string UniqueId { get; set; }
        public bool? Active { get; set; }
        [Required]
        public string LicenseKey { get; set; }
        public static MachineDto MapToMachineDto(Machine machine)
        {
            return new MachineDto()
            {
                Name = machine.Name,
                UniqueId = machine.UniqueId,
                Active = machine.Active,
                Phone = machine.Phone,
                Email = machine.Email
            };
        }
    }
}
