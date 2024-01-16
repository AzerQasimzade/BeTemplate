using ImtahanTest1.Areas.ViewModels;
using ImtahanTest1.DAL;
using ImtahanTest1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImtahanTest1.Areas.BeAdmin.Controllers
{
    [Area("BeAdmin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.Employees.ToListAsync();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM createEmployeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

        }

    }
}
