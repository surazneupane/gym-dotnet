using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GymManagement.Controllers
{
    public class ManageMembershipTypesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<MembershipType> membershipTypes = db.MembershipTypes.ToList();
            ViewBag.ShowRegisterLoginLinks = true;
            return View(membershipTypes);
        }

        // Example action to create a new membership type
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MembershipType membershipType)
        {
            if (ModelState.IsValid)
            {
                db.MembershipTypes.Add(membershipType);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(membershipType);
        }

        [HttpPost]
        public ActionResult Edit(MembershipType model)
        {
            if (ModelState.IsValid)
            {
                // Update the record in the database
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model); // If ModelState is not valid, stay on the Edit view with validation errors
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ShowRegisterLoginLinks = true;
            var membershipType = db.MembershipTypes.Find(id);

            if (membershipType == null)
            {
                return HttpNotFound(); // Handle the case where the item is not found
            }

            return View(membershipType); // Pass the model to the Edit view
        }

        [HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            MembershipType membershipType = db.MembershipTypes.Find(id);
            if (membershipType == null)
            {
                return HttpNotFound();
            }

            db.MembershipTypes.Remove(membershipType);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MembershipType membershipType = db.MembershipTypes.Find(id);
            if (membershipType == null)
            {
                return HttpNotFound();
            }

            db.MembershipTypes.Remove(membershipType);
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
