using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymManagement.Controllers
{
    public class CheckInController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(Guid userId)
        {
            ViewBag.ShowRegisterLoginLinks = true;

            var user = db.Users.FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
                var memberships = db.Memberships.Where(m => m.MemberID == userId).ToList();
                var membershipTypes = db.MembershipTypes.ToList();

               

                MemberDashboardViewModel viewModel = new MemberDashboardViewModel
                {
                    User = user,
                    Memberships = memberships,
                    MembershipTypes = membershipTypes,
                  
                };

                return View(viewModel);
            }

            TempData["Message"] = "User not found.";
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult CheckIn(Guid userId)
        {
            // Assuming db is your DbContext
            var user = db.Users.FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
                // Create a new Attendance record
                Attendance attendance = new Attendance
                {
                    MemberID = user.UserId,
                    CheckInDateTime = DateTime.Now
                };

                // Add attendance to the DbSet and save changes
                db.Attendances.Add(attendance);
                db.SaveChanges();

                // Redirect back to the Index action with the current user's ID
                return RedirectToAction("Index", new { userId = user.UserId });
            }

            // Handle the case where the user with the provided userId does not exist
            TempData["Message"] = "User not found.";
            return RedirectToAction("Login"); // or any other appropriate action
        }

    }
}