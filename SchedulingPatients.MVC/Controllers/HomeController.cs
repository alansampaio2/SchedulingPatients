using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchedulingPatients.MVC.Data;
using SchedulingPatients.MVC.Models;
using SchedulingPatients.MVC.Models.PatientViewModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingPatients.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScheduleContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ScheduleContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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

        public async Task<ActionResult> About()
        {
            IQueryable<ScheduleDateGroup> data = from schedule in _context.Schedules
                                                 group schedule by schedule.Date into dateGroup
                                                 select new ScheduleDateGroup()
                                                 {
                                                     ScheduleDate = dateGroup.Key,
                                                     PatientCount = dateGroup.Count()
                                                 };
            return View(await data.AsNoTracking().ToListAsync());
        }
    }
}
