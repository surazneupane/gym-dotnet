using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymManagement.Controllers
{
    public class ManageMembershipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.ShowRegisterLoginLinks = true;

            // Assuming you have a method to retrieve MembershipTypes and Memberships
            var viewModel = new MembershipViewModel
            {
                Users = GetUsers(),
                MembershipTypes = GetMembershipTypes(),
                Memberships = GetMemberships()
            };

            return View(viewModel);
        }

        private List<Register> GetUsers()
        {
            return db.Users.ToList(); // Assuming _context is your DbContext
        }



        private List<MembershipType> GetMembershipTypes()
        {
            return db.MembershipTypes.ToList();
        }

        private List<Membership> GetMemberships()
        {
            return db.Memberships.ToList();
        }


        // Create Membership (GET)
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Membership model)
        {
            if (ModelState.IsValid)
            {
                db.Memberships.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Create", model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.ShowRegisterLoginLinks = true;
            var membership = db.Memberships.Find(id);

            if (membership == null)
            {
                return HttpNotFound();
            }

            var membershipTypes = db.MembershipTypes.ToList();

            var viewModel = new EditMembershipViewModel
            {
                Membership = membership,
                MembershipTypes = membershipTypes
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Membership membership)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membership).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(membership);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Membership membership = db.Memberships.Find(id);
            if (membership == null)
            {
                return HttpNotFound();
            }

            db.Memberships.Remove(membership);
            db.SaveChanges();

            return RedirectToAction("Index"); // Redirect to the Index action
        }



    }
}