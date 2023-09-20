using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GymManagement.Models;

namespace GymManagement.Controllers
{
    public class SummaryReportController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ShowRegisterLoginLinks = false;

            var summaryData = GetSummaryData();
            var interestRecordData = GetInterestRecordData();

            var viewModel = new SummaryReportViewModel
            {
                SummaryData = summaryData,
                InterestRecordData = interestRecordData
            };

            return View(viewModel);
        }

        private List<SummaryDataViewModel> GetSummaryData()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Memberships
                    .GroupBy(m => m.MembershipType.Type)
                    .Select(g => new SummaryDataViewModel
                    {
                        MembershipType = g.Key,
                        MemberCount = g.Count()
                    })
                    .ToList();
            }
        }

        private List<InterestRecordViewModel> GetInterestRecordData()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.InterestRecords
                    .Select(ir => new InterestRecordViewModel
                    {
                        SessionID = ir.SessionID,
                        MemberID = ir.MemberID,
                        ClassName = context.TrainingSessions
                                      .Where(ts => ts.ID == ir.SessionID)
                                      .Select(ts => ts.Class.ClassName)
                                      .FirstOrDefault(),
                        UserName = context.Users
                                     .Where(u => u.UserId == ir.MemberID)
                                     .Select(u => u.UserName)
                                     .FirstOrDefault()
                    })
                    .ToList();
            }
        }
    }
}
