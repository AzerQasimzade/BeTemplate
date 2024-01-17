using ImtahanTest1.Areas.ViewModels;
using ImtahanTest1.DAL;
using ImtahanTest1.Models;
using ImtahanTest1.Utilities.Enums;
using ImtahanTest1.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImtahanTest1.Areas.BeAdmin.Controllers
{
    [Area("BeAdmin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
            if (!createEmployeeVM.Photo.ValidateFileType(FileHelper.Image))
            {
                ModelState.AddModelError("Photo", "File type is not macthing");
                return View();
            }
            if (!createEmployeeVM.Photo.ValidateFileSize(SizeHelper.mb))
            {
                ModelState.AddModelError("Photo", "File size is not macthing");
                return View();
            }
            
            string filename=Guid.NewGuid().ToString()+createEmployeeVM.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "assets", "img",filename);
            FileStream file = new FileStream(path, FileMode.Create);
            await createEmployeeVM.Photo.CopyToAsync(file);
            Employee employee = new Employee
            {
                Image = filename,
                Name = createEmployeeVM.Name,   
                Profession = createEmployeeVM.Profession,
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home"); 
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id<=0)
            {
                return BadRequest();
            }
            Employee existed= await _context.Employees.FirstOrDefaultAsync(c=>c.Id==id);
            if (existed is null)
            {
                return NotFound();
            }
            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM
            {
                Name = existed.Name,
                Profession = existed.Profession,
                Photo = existed.Photo
            };
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateEmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Employee existed = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);
            if (existed is null)
            {
                return NotFound();
            }
            if (employeeVM.Photo is not null)
            {
                if (!employeeVM.Photo.ValidateFileType(FileHelper.Image))
                {
                    ModelState.AddModelError("Photo", "File type is not macthing");
                    return View();
                }
                if (!employeeVM.Photo.ValidateFileSize(SizeHelper.mb))
                {
                    ModelState.AddModelError("Photo", "File size is not macthing");
                    return View();
                }

                string filename = Guid.NewGuid().ToString() + employeeVM.Photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets", "img", filename);
                FileStream file = new FileStream(path, FileMode.Create);
                await employeeVM.Photo.CopyToAsync(file);
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "img");
                existed.Image = filename;
            }
            existed.Profession=employeeVM.Profession;
            existed.Name=employeeVM.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
