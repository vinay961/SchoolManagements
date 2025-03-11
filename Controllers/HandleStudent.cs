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
            // here we have to check for admin authentication
            var admin = HttpContext.Session.GetString("Admin");
            if (admin == null)
            {
                return RedirectToAction("Login", "HandleAdmin");
            }
            var student = dbContext.Students.Find(Id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                // Fetch the existing student from the database
                var existingStudent = await dbContext.Students.FirstOrDefaultAsync(s => s.Email == student.Email);

                if (existingStudent == null)
                {
                    ViewBag.Message = "Student not found";
                    return RedirectToAction("FailedPage");
                }

                // Fetch the corresponding class
                var classEntity = await dbContext.Class.FirstOrDefaultAsync(c => c.classNumber == student.Standard);
                if (classEntity == null)
                {
                    ViewBag.Message = "Class not found";
                    return RedirectToAction("FailedPage");
                }

                // Update the existing student details
                existingStudent.Name = student.Name;
                existingStudent.FatherName = student.FatherName;
                existingStudent.Email = student.Email;
                existingStudent.Standard = student.Standard;
                existingStudent.ClassId = classEntity.Id;

                // Update and save changes
                dbContext.Students.Update(existingStudent);
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
            var admin = HttpContext.Session.GetString("Admin");
            if (admin == null)
            {
                return RedirectToAction("Login", "HandleAdmin");
            }
            var student = await dbContext.Students.FindAsync(Id);
            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }
    }
}
