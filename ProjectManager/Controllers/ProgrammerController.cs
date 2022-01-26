using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data;
using ProjectManager.Models;
using System.Collections.Generic;

namespace ProjectManager.Controllers
{
    public class ProgrammerController : Controller
    {
        private readonly ManagerAppDbContext Db;

        public ProgrammerController(ManagerAppDbContext db)
        {
            Db = db;
        }

        public List<Programmer> Programmers { get; set; }

        public IActionResult Index()
        {
            IEnumerable<Programmer> programmerList = Db.Programmers;
            return View(programmerList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Programmer programmer)
        {
            Db.Programmers.Add(programmer);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Programmer programmer = Db.Programmers.Find(id);
            if (programmer == null)
            {
                return NotFound();
            }
            return View(programmer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostDelete(Programmer programmer)
        {

            if (programmer == null)
            {
                return NotFound();
            }
            Db.Programmers.Remove(programmer);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Programmer programmer = Db.Programmers.Find(id);
            if (programmer == null)
            {
                return NotFound();
            }
            return View(programmer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Programmer programmer)
        {
            Db.Programmers.Update(programmer);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
