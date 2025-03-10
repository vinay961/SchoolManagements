using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    public class HandleStudent : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public HandleStudent(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }

        public IActionResult Edit(int Id)
        {
            var student = dbContext.Students.Find(Id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Students student)
        {
            if (ModelState.IsValid)
            {
                dbContext.Students.Update(student);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(student);
        }

        public IActionResult Details(int Id)
        {
            var student = dbContext.Students.Find(Id);
            return View(student);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var student = await dbContext.Students.FindAsync(Id);
            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}
