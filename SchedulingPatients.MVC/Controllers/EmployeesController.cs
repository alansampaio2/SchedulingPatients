using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchedulingPatients.MVC.Data;
using SchedulingPatients.MVC.Models;
using SchedulingPatients.MVC.Models.PatientViewModel;

namespace SchedulingPatients.MVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ScheduleContext _context;

        public EmployeesController(ScheduleContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int? id, int? serviceID)
        {
            var viewModel = new EmployeeIndexData();

            viewModel.Employees = await _context.Employees
                .Include(i => i.OfficeAssignment)
                .Include(i => i.ServiceAssignments).ThenInclude(i => i.Service).ThenInclude(i => i.Schedules).ThenInclude(i => i.Patient)
                .Include(i => i.ServiceAssignments).ThenInclude(i => i.Service).ThenInclude(i => i.Department)
                .AsNoTracking().OrderBy(i => i.FirstName).ToListAsync();

            if (id != null)
            {
                ViewData["EmployeeID"] = id.Value;
                Employee employee = viewModel.Employees.Where(
                    i => i.ID == id.Value).Single();
                viewModel.Services = employee.ServiceAssignments.Select(s => s.Service);
            }

            if (serviceID != null)
            {
                ViewData["ServiceID"] = serviceID.Value;
                var selectedService = viewModel.Services.Where(x => x.ServiceID == serviceID).Single();
                await _context.Entry(selectedService).Collection(x => x.Schedules).LoadAsync();

                foreach (Schedule schedule in selectedService.Schedules)
                {
                    await _context.Entry(schedule).Reference(x => x.Patient).LoadAsync();
                }
                viewModel.Schedules = selectedService.Schedules;
            }

            return View(viewModel);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var employee = new Employee();
            employee.ServiceAssignments = new List<ServiceAssignment>();
            PopulateAssignedServiceData(employee);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee, string[] selectedServices)
        {
            if (selectedServices != null)
            {
                employee.ServiceAssignments = new List<ServiceAssignment>();
                foreach (var service in selectedServices)
                {
                    var serviceToAdd = new ServiceAssignment { EmployeeID = employee.ID, ServiceID = int.Parse(service) };
                    employee.ServiceAssignments.Add(serviceToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedServiceData(employee);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(i => i.OfficeAssignment)
                .Include(i => i.ServiceAssignments).ThenInclude(i => i.Service)
                .AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);

            if (employee == null)
            {
                return NotFound();
            }

            PopulateAssignedServiceData(employee);
            return View(employee);
        }

        private void PopulateAssignedServiceData(Employee employee)
        {
            var allServices = _context.Services;
            var instructorCourses = new HashSet<int>(employee.ServiceAssignments.Select(c => c.ServiceID));
            var viewModel = new List<AssignedServiceData>();
            foreach (var course in allServices)
            {
                viewModel.Add(new AssignedServiceData
                {
                    ServiceID = course.ServiceID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.ServiceID)
                });
            }
            ViewData["Services"] = viewModel;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedServices)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeToUpdate = await _context.Employees
                .Include(i => i.OfficeAssignment)
                .Include(i => i.ServiceAssignments).ThenInclude(i => i.Service)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (await TryUpdateModelAsync<Employee>(employeeToUpdate, "", i => i.FirstName, i => i.LastName, i => i.HireDate, id => id.Register, i => i.OfficeAssignment))
            {
                if (String.IsNullOrWhiteSpace(employeeToUpdate.OfficeAssignment?.Location))
                {
                    employeeToUpdate.OfficeAssignment = null;
                }

                UpdateInstructorCourses(selectedServices, employeeToUpdate);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }

            UpdateInstructorCourses(selectedServices, employeeToUpdate);
            PopulateAssignedServiceData(employeeToUpdate);
            return View(employeeToUpdate);
        }

        private void UpdateInstructorCourses(string[] selectedServices, Employee employeeToUpdate)
        {
            if (selectedServices == null)
            {
                employeeToUpdate.ServiceAssignments = new List<ServiceAssignment>();
                return;
            }

            var selectedServicesHS = new HashSet<string>(selectedServices);
            var employeeServices = new HashSet<int>(employeeToUpdate.ServiceAssignments.Select(c => c.Service.ServiceID));

            foreach (var service in _context.Services)
            {
                if (selectedServicesHS.Contains(service.ServiceID.ToString()))
                {
                    if (!employeeServices.Contains(service.ServiceID))
                    {
                        employeeToUpdate.ServiceAssignments.Add(new ServiceAssignment { EmployeeID = employeeToUpdate.ID, ServiceID = service.ServiceID });
                    }
                }
                else
                {

                    if (employeeServices.Contains(service.ServiceID))
                    {
                        ServiceAssignment courseToRemove = employeeToUpdate.ServiceAssignments.FirstOrDefault(i => i.ServiceID == service.ServiceID);
                        _context.Remove(courseToRemove);
                    }
                }
            }
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await _context.Employees
                .Include(i => i.ServiceAssignments)
                .SingleAsync(i => i.ID == id);

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}
