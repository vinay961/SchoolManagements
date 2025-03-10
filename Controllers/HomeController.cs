using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<HomeController> _logger;
    IWebHostEnvironment env; // This line help us to get the path of wwwroot folder. 

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IWebHostEnvironment env)
    {
        this.dbContext = dbContext;
        _logger = logger;
        this.env = env;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Registration()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Registration(StudentViewModel s)
    {
        if(!ModelState.IsValid)
        {
            return RedirectToAction("FailedPage");
        }
        string fileName = "";
        if(s.ImageFile != null)
        {
            string folder = Path.Combine(env.WebRootPath, "images");
            fileName = Guid.NewGuid().ToString() + "_" + s.ImageFile.FileName;
            string filePath = Path.Combine(folder, fileName);
            s.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create)); // Images are uploaded to the wwwroot/images folder.

            Students student = new Students
            {
                Name = s.Name,
                FatherName = s.FatherName,
                Email = s.Email,
                Standard = s.Standard,
                ProfileImage = fileName
            };
            dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("SuccessPage");
        }
        return RedirectToAction("Index");
    }

    public IActionResult SuccessPage()
    {
        return View();
    }
    public IActionResult FailedPage()
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
}
