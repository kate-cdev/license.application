using license.application.Data;
using license.application.Dto;
using license.application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license.application.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtConstants.AuthSchemes)]
    public class MachineController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MachineController(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        [Route("api/machine/{uniqueId}")]
        [HttpGet]
        public async Task<IActionResult> GetMachine(string uniqueId)
        {
            var machine = await _applicationDbContext.Machines.FirstOrDefaultAsync(m => m.UniqueId.ToLower().Equals(uniqueId.ToLower()));

            if(machine != null)
            {
                return Ok(MachineDto.MapToMachineDto(machine));
            }

            return NotFound();
        }

        [Route("api/machine")]
        [HttpPost]
        public async Task<IActionResult> CreateMachine([FromBody] MachineDto machineDto)
        {
            try
            {
                License license = null;
                if (ModelState.IsValid)
                {
                    license = await _applicationDbContext.License.Include(l => l.Machine)
                     .FirstOrDefaultAsync(l => l.Key.ToLower().Equals(machineDto.LicenseKey.ToLower()) && l.Machine == null);

                    if (license == null)
                        ModelState.AddModelError(nameof(machineDto.LicenseKey), "Incorrect License Key");
                }


                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values);

                Machine machine = null;
                if (await _applicationDbContext.Machines.AnyAsync(m => m.UniqueId.Equals(machineDto.UniqueId)))
                {
                    //ModelState.AddModelError(nameof(machineDto.UniqueId), "This machine already registered");
                    machine = await _applicationDbContext.Machines.FirstOrDefaultAsync(m => m.UniqueId.Equals(machineDto.UniqueId));
                    machine.License = license;
                    machine.Name = machineDto.Name;
                    machine.Email = machineDto.Email;
                    machine.Phone = machine.Phone;
                    machine.Active = license.IsActive();

                    _applicationDbContext.Machines.Update(machine);
                } else
                {
                    machine = Machine.MapToMachine(machineDto);
                    machine.License = license;
                    machine.Active = license.IsActive();

                    _applicationDbContext.Machines.Add(machine);
                }

                await _applicationDbContext.SaveChangesAsync();

                return new ObjectResult(MachineDto.MapToMachineDto(machine)) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [Route("api/machine/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateMachine(int id)
        {
            var machine = await _applicationDbContext.Machines.FirstOrDefaultAsync(x => x.Id == id);
            if (machine == null) return BadRequest();

            machine.Active = !machine.Active;

            await _applicationDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
