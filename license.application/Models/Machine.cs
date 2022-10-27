using license.application.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace license.application.Models
{
    public class Machine
    {
        public Machine()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [Required, MaxLength(450)]
        public string UniqueId { get; set; }
        public bool Active { get; set; }

        public int? LicenseId { get; set; }
        public License License { get; set; }

        public static Machine MapToMachine(MachineDto machineDto)
        {
            return new Machine()
            {
                Name = machineDto.Name,
                UniqueId = machineDto.UniqueId,
                Active = false,
                Phone = machineDto.Phone,
                Email = machineDto.Email
            };
        }

    }
}
