using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchedulingPatients.MVC.Data;
using SchedulingPatients.MVC.Models;

namespace SchedulingPatients.MVC.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ScheduleContext _context;

        public ServicesController(ScheduleContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var services = _context.Services.Include(s => s.Department).AsNoTracking();
            return View(await services.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Department).AsNoTracking()
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceID,Title,DepartmentID")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(service.DepartmentID);
            return View(service);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.AsNoTracking().FirstOrDefaultAsync(m => m.ServiceID == id);
            if (service == null)
            {
                return NotFound();
            }
            PopulateDepartmentsDropDownList(service.DepartmentID);
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceToUpdate = await _context.Services.FirstOrDefaultAsync(c => c.ServiceID == id);

            if(await TryUpdateModelAsync<Service>(serviceToUpdate, "", c=>c.DepartmentID, c=>c.Title))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Incapaz de salvar as alterações. " +
                        "Tente novamente e se o problema persistir, " +
                        "consulte o administrador do sistema.");
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateDepartmentsDropDownList(serviceToUpdate.DepartmentID);
            return View(serviceToUpdate);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Departments
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectedDepartment);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Department).AsNoTracking()
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ServiceID == id);
        }
    }
}
