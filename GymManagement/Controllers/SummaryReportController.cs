using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymManagement.Controllers
{
    public class SummaryReportController : Controller
    {
        // GET: SummaryReport
        public ActionResult Index()
        {
            ViewBag.ShowRegisterLoginLinks = false;
            var summaryData = GetSummaryData(); // Retrieve your data here
            return View(summaryData);
           
        }

      

        private List<SummaryReportViewModel> GetSummaryData()
        {
            var summaryData = new List<SummaryReportViewModel>();

            using (var context = new ApplicationDbContext()) // Replace with your actual DbContext
            {
                summaryData = context.Memberships
                    .GroupBy(m => m.MembershipType.Type)
                    .Select(g => new SummaryReportViewModel
                    {
                        MembershipType = g.Key,
                        MemberCount = g.Count()
                    })
                    .ToList();
            }

            return summaryData;
        }

    }
}