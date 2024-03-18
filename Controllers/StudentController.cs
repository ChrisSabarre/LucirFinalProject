using LucirWeb_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace LucirWeb_MVC.Controllers
{
    public class StudentController : Controller
    {
        [Authorize]
        public IActionResult SearchResult()
        {
            string studentsJson = TempData["StudentsList"] as string;
            List<Student> students = JsonSerializer.Deserialize<List<Student>>(studentsJson); 
            return View(students);
        }

        private Student o = new();
        [Authorize]
        public IActionResult Index()
        {
            List<Student> l = o.GetAllStudents();
            return View(l);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Student model)
        {
            model.Delete();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult SearchResult(Student model)
        {
            try
            {
                model.HistoryDb();
                List<Student> studentsList = model.GetStudentWithName();
                string studentsJson = JsonSerializer.Serialize(studentsList);
                TempData["StudentsList"] = studentsJson; // Store JSON string in TempData
                return RedirectToAction("SearchResult");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ErrKey", ex.Message);
                return View(model);
            }
        }
        [Authorize]
        public IActionResult Individual()
        {
            Student s = new Student();
            return View(s);
        }

        [Authorize]

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Student s = o.GetStudent((int)id);
                if (s.StudentID == 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(s);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Update(Student model)
        {
            try
            {
                model.Update(model.OldSid);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ErrKey", ex.Message);
                return View(model);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            Student s = new Student();
            return View(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Add(Student model)
        {
            try
            {
                model.Add();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ErrKey", ex.Message);
                return View(model);

            }
        }
    }
}
