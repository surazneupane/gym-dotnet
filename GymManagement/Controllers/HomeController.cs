using GymManagement.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymManagement.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult Index()
		{
            ViewBag.ShowRegisterLoginLinks = true;
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";
			ViewBag.ShowRegisterLoginLinks = true;
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";
			ViewBag.ShowRegisterLoginLinks = true;
			return View();
		}

		//[Authorize]
		// Requires authentication to access this action
		public ActionResult Dashboard()
		{
			ViewBag.ShowRegisterLoginLinks = true;
			return View();
		}

		public ActionResult MemberDashboard(Guid userId)
		{
			ViewBag.ShowRegisterLoginLinks = true;

			var user = db.Users.FirstOrDefault(u => u.UserId == userId);

			if (user != null)
			{
				var memberships = db.Memberships.Where(m => m.MemberID == userId).ToList();
				var membershipTypes = db.MembershipTypes.ToList();

				var viewModel = new MemberDashboardViewModel
				{
					User = user,
					Memberships = memberships,
					MembershipTypes = membershipTypes  // Add this line to pass membership types to the view
				};

				return View(viewModel);
			}

			// Handle the case where the user with the provided userId does not exist
			TempData["Message"] = "User not found.";
			return RedirectToAction("Login"); // or any other appropriate action
		}


		private Guid GetCurrentMemberId()
		{
			if (User.Identity.IsAuthenticated)
			{
				return Guid.Parse(User.Identity.GetUserId());
			}
			else
			{
				// Handle the case where the user is not authenticated
				// For example, you might redirect them to a login page
				// or return a default value
				return Guid.Empty; // This is just an example, use an appropriate default value
			}
		}


		private List<Membership> GetMembershipsForMember(Guid memberId)
		{
			// Implement the logic to query the database and retrieve memberships for the given member ID
			return db.Memberships.Where(m => m.MemberID == memberId).ToList();
		}




	}
}
