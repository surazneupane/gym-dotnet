using GymManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace GymManagement.Controllers
{
    public class ManageTrainingSessionsController : Controller
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.ShowRegisterLoginLinks = true;
            var viewModel = new TrainingSessionViewModel
            {
                Classes = GetClasses(),
                Staff = GetStaff(),
                TrainingSessions = GetTrainingSessions(),
                GetUsernameForStaffID = GetUsernameForStaffID  // Pass the method to the view
            };

            return View(viewModel);
        }

        public List<Class> GetClasses()
        {
            return dbContext.Classes.ToList();
        }

        public List<Register> GetStaff()
        {
            return dbContext.Users.Where(u => u.IsGymStaff).ToList();
        }

        public List<TrainingSession> GetTrainingSessions()
        {
            return dbContext.TrainingSessions.Include(ts => ts.Class).Include(ts => ts.Staff).ToList();
        }


        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(TrainingSession model)
        {
            if (ModelState.IsValid)
            {
                dbContext.TrainingSessions.Add(model);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Create", model);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.ShowRegisterLoginLinks = true;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrainingSession trainingSession = dbContext.TrainingSessions.Find(id);


            if (trainingSession == null)
            {
                return HttpNotFound();
            }

            TrainingSessionEditViewModel viewModel = new TrainingSessionEditViewModel
            {
                TrainingSession = trainingSession,
                Staff = GetStaff(),
                SelectedClassName = GetClassNameForClassID(trainingSession.ClassID),
                SelectedStaffUsername = GetUsernameForStaffID(trainingSession.StaffID),
            };

            return View(viewModel);
        }


        private string GetClassNameForClassID(int classID)
        {
            // Assuming dbContext is your EF DbContext
            var className = dbContext.Classes
                .Where(c => c.ID == classID)
                .Select(c => c.ClassName)
                .FirstOrDefault();

            return className;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrainingSessionEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Get the existing training session from the database
                var existingTrainingSession = dbContext.TrainingSessions.Find(viewModel.TrainingSession.ID);

                // Update properties
                existingTrainingSession.Capacity = viewModel.TrainingSession.Capacity;
                
                existingTrainingSession.DateTime = viewModel.TrainingSession.DateTime;

                //if (viewModel.TrainingSession.DateTime != existingTrainingSession.DateTime)
                //{
                //    existingTrainingSession.DateTime = viewModel.TrainingSession.DateTime;
                //}

                // Update other properties as needed

                // Save changes to the database
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            viewModel.Staff = GetStaff();

            return View(viewModel);
        }



        private TrainingSession GetTrainingSession(int? id)
        {
            return dbContext.TrainingSessions.Include(ts => ts.Class).Include(ts => ts.Staff).FirstOrDefault(ts => ts.ID == id);
        }

     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingSession trainingSession = dbContext.TrainingSessions.Find(id);
            if (trainingSession == null)
            {
                return HttpNotFound();
            }
            return View(trainingSession);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainingSession trainingSession = dbContext.TrainingSessions.Find(id);
            dbContext.TrainingSessions.Remove(trainingSession);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        private string GetUsernameForStaffID(Guid staffID)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.UserId == staffID);

            if (user != null)
            {
                return user.UserName;
            }

            return "Unknown"; // Or some default value if user is not found
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}