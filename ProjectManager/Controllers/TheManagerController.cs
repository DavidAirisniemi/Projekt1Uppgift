using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Controllers
{
    public class TheManagerController : Controller
    {
        private readonly ManagerAppDbContext Db;

        public TheManagerController(ManagerAppDbContext db)
        {
            Db = db;
        }

        public List<TheManager> Manager { get; set; }

        public IActionResult Index()
        {
            var manager = Db.Manager
                .Include(b => b.Programmer)
                .Include(b => b.Project);
            return View(manager.ToList());
        }

        public IActionResult Create()
        {
            ViewData["ProgrammerId"] = new SelectList(Db.Programmers, "Id", "FirstName");
            ViewData["ProjectId"] = new SelectList(Db.Projects, "Id", "ProjectName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TheManager manager)
        {
            Db.Manager.Add(manager);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public IActionResult GoToDelete()
        {
            int progid = Convert.ToInt32(Request.Form["ProgrammerId"]);
            int projid = Convert.ToInt32(Request.Form["ProjectId"]);

            if (progid == 0 || projid == 0)
            {
                return NotFound();
            }
            TheManager manager = Db.Manager.Find(progid, projid);
            if (manager == null)
            {
                return NotFound();
            }
            TempData["ProgrammerId"] = progid;
            TempData["ProjectId"] = projid;
            return View("Delete", manager);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostDelete()
        {
            int progid = (int)TempData["ProgrammerId"];
            int projid = (int)TempData["ProjectId"];

            TheManager manager = Db.Manager.Find(projid, progid);
            
            Db.Manager.Remove(manager);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult GoToUpdate()
        {
            int progid = Convert.ToInt32(Request.Form["ProgrammerId"]);
            int projid = Convert.ToInt32(Request.Form["ProjectId"]);
            if (progid == 0 || projid == 0)
            {
                return NotFound();
            }
            TheManager manager = Db.Manager.Find(progid, projid);
            if (manager == null)
            {
                return NotFound();
            }
            TempData["ProgrammerId"] = progid;
            TempData["ProjectId"] = projid;
            return View("Update",manager);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update()
        {
            int progid = (int)TempData["ProgrammerId"];
            int projid = (int)TempData["ProjectId"];
            TheManager manager = Db.Manager.Find(progid, projid);
            Db.Manager.Update(manager);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Login(int id)
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetInt32("LoginId", id);
            var programmer = Db.Programmers.Find(id);
            HttpContext.Session.SetString("Name", $"Logged in as {programmer.FirstName} {programmer.LastName}");
            if (programmer.AdminLogin)
            {
                HttpContext.Session.SetInt32("AdminId", 1);
            }
            return RedirectToAction("Index");
        }
    }
}
