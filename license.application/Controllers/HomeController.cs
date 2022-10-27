using license.application.Data;
using license.application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace license.application.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _applicationDbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var machines = await _applicationDbContext.Machines.Include(m => m.License).OrderByDescending(m => m.Id).ToListAsync();
            return View(machines);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Edit(int id)
        {
            var machine = _applicationDbContext.Machines.FirstOrDefault(m => m.Id == id);
            var license = _applicationDbContext.License.Where(l => l.Machine == null || l.Machine.Id == id).ToList();

            return View(new MachineViewModel() { Machine = machine, Licenses = license});
        }

        // POST: License/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Machine machine)
        {
            try
            {
                if (machine.LicenseId.HasValue)
                {
                    var license = await _applicationDbContext.License.FirstOrDefaultAsync(l => l.Id == machine.LicenseId.Value);
                    machine.Active = license.IsActive();
                }
                else machine.Active = false;

                _applicationDbContext.Machines.Update(machine);
                _applicationDbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: LicenseController/Delete/5
        public ActionResult Delete(int id)
        {
            var machine = _applicationDbContext.Machines.FirstOrDefault(l => l.Id == id);
            _applicationDbContext.Machines.Remove(machine);

            _applicationDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
