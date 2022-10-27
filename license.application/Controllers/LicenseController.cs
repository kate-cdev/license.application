using license.application.Data;
using license.application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license.application.Controllers
{
    [Authorize]
    public class LicenseController : Controller
    {
        private ApplicationDbContext _applicationDbContext;

        public LicenseController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // GET: License
        public async Task<ActionResult> Index()
        {
            var license = await _applicationDbContext.License.Include(l => l.Machine).OrderByDescending(l => l.CreatedUtc).ToListAsync();

            return View(license);
        }

        // GET: License/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: License/Create
        public ActionResult Create(int? id)
        {
            License newlicense = null;

            if (id.HasValue)
                newlicense = _applicationDbContext.License.FirstOrDefault(l => l.Id == id.Value);
            else
              newlicense = new License() {
                Key = Guid.NewGuid().ToString(),
                ExpireDate = DateTime.Now.AddDays(90)
                };
            
            return View(newlicense);
        }

        // POST: License/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(License license)
        {
            try
            {
                if(license.Id > 0)
                {
                    _applicationDbContext.License.Update(license);
                    var machine = await _applicationDbContext.Machines.FirstOrDefaultAsync(m => m.LicenseId == license.Id);
                    if (machine != null)
                    {
                        machine.Active = license.IsActive();
                        _applicationDbContext.Machines.Update(machine);
                    }
                }
                else 
                    await _applicationDbContext.License.AddAsync(license);
                
                _applicationDbContext.SaveChanges();
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: License/Edit/5
        public ActionResult Edit(int id)
        {
           var newlicense = _applicationDbContext.License.FirstOrDefault(l => l.Id == id);

            return View("Create", newlicense);
        }

        // POST: LicenseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LicenseController/Delete/5
        public ActionResult Delete(int id)
        {
            var newlicense = _applicationDbContext.License.FirstOrDefault(l => l.Id == id);
            _applicationDbContext.License.Remove(newlicense);

            _applicationDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // POST: LicenseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
