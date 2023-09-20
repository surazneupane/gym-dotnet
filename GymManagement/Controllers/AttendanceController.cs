using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GymManagement.Models;

namespace GymManagement.Controllers
{
    public class AttendanceController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ShowRegisterLoginLinks = false;

            var attendanceData = GetAttendanceData();

            return View(attendanceData);
        }

        private List<Attendance> GetAttendanceData()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Attendances.ToList();
            }
        }
    }
}
