using GymManagement.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace GymManagement.Controllers
{
    public class TrainingSessionController : Controller
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

                var trainingSessions = db.TrainingSessions
                    .Include(ts => ts.Class)
                    .ToList();

                MemberDashboardViewModel viewModel = new MemberDashboardViewModel
                {
                    User = user,
                    Memberships = memberships,
                    MembershipTypes = membershipTypes,
                    TrainingSessions = trainingSessions.Select(ts => new TrainingSessionNewViewModel
                    {
                        TrainingSessions = new List<TrainingSession> { ts }
                    }).ToList()
                };

                return View(viewModel);
            }

            TempData["Message"] = "User not found.";
            return RedirectToAction("Login");
        }






        private Guid GetCurrentUserId()
        {
            // Implement your logic to get the current user's ID
            // For example, if you're using session:
            var userIdString = Session["UserId"] as string;
            if (Guid.TryParse(userIdString, out Guid userId))
            {
                return userId;
            }
            return Guid.Empty; // Return a default value if user ID is not found
        }



        // ... Rest of the controller code



        //[HttpPost]
        //public ActionResult ExpressInterest(int sessionId)
        //{
        //    ExpressInterestInSession(sessionId);
        //    return RedirectToAction("Index");
        //}

        private List<TrainingSession> GetAvailableTrainingSessions()
        {
            return db.TrainingSessions.ToList(); 
        }

        private void ExpressInterestInSession(int sessionId)
        {
          
            TrainingSession session = db.TrainingSessions.Find(sessionId);
            if (session != null && session.Capacity > 0)
            {
                session.Capacity--;
                db.SaveChanges();
            }
        }

        public ActionResult ExpressInterest(int sessionId, Guid userId)
        {
            if (userId != Guid.Empty)
            {
                // Save the interest record to the database
                InterestRecord interestRecord = new InterestRecord
                {
                    MemberID = userId,
                    SessionID = sessionId
                };

                db.InterestRecords.Add(interestRecord);
                db.SaveChanges();

                return RedirectToAction("Index", new { userId = userId });
            }

            TempData["Message"] = "User not found.";
            return RedirectToAction("Login");
        }



        private Guid GetCurrentMemberId()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Guid.Parse(User.Identity.GetUserId());
            }
            else
            {
                return Guid.Empty;
            }
        }

      




    }
}