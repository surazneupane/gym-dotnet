using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymManagement.Controllers
{
    public class ManageClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManageClasses
        public ActionResult Index()
        {
            var classes = db.Classes.ToList();
            ViewBag.ShowRegisterLoginLinks = true;
            return View(classes);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Class model)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            ViewBag.ShowRegisterLoginLinks = true;
            Class classItem = db.Classes.Find(id);
            if (classItem == null)
            {
                return HttpNotFound();
            }
            return View(classItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Class classItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Class classItem = db.Classes.Find(id);
            if (classItem == null)
            {
                return HttpNotFound();
            }

            db.Classes.Remove(classItem);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}