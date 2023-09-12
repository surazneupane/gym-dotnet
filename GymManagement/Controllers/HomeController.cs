using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymManagement.Controllers
{
	public class HomeController : Controller
	{
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
	}
}