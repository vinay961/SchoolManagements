using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    public class HandleAdmin : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public HandleAdmin(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Admin a)
        {
            if (a.Email == "admin@gmail.com" && a.Password == "admin123")
            {
                HttpContext.Session.SetString("Admin", a.Email);
                return RedirectToAction("Admin"); // Redirect to Admin Panel
            }

            var admin = dbContext.Admin.FirstOrDefault(ad => ad.Email == a.Email && ad.Password == a.Password);
            if (admin != null)
            {
                return RedirectToAction("Admin");
            }

            ViewBag.Message = "Invalid Email or Password";
            return View("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login");
        }

        public IActionResult Admin()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult ClassList()
        {
            var classes = dbContext.Class
                .Include(c => c.Students) 
                .ToList();
            return View(classes);
        }
        public IActionResult AddClass()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddClass(Class c)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("FailedPage");
            }
            dbContext.Class.Add(c);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Admin");
        }
        public IActionResult ViewClassStudents(int Id)
        {
            // Get the class by Id
            var c = dbContext.Class
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Id == Id);
            if (c == null)
            {
                ViewBag.Message = "Class not found";
                return RedirectToAction("ClassList");
            }
            // now return the students of this class
            return View(c.Students);
        }
    }
}
