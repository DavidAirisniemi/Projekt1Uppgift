using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ManagerAppDbContext Db;

        public ProjectController(ManagerAppDbContext db)
        {
            Db = db;
        }

        public List<Project> Projects { get; set; }

        public IActionResult Index()
        {
            IEnumerable<Project> projectList = Db.Projects;
            return View(projectList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            Db.Projects.Add(project);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Project project = Db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostDelete(Project project)
        {

            if (project == null)
            {
                return NotFound();
            }
            Db.Projects.Remove(project);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Project project = Db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Project project)
        {
            Db.Projects.Update(project);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
