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
        private ApplicationDbContext dbContext = new ApplicationDbContext(); // Assuming you have a DbContext named ApplicationDbContext

        public ActionResult Index()
        {
            ViewBag.ShowRegisterLoginLinks = true;
            var viewModel = new TrainingSessionViewModel
            {
                Classes = GetClasses(), // Implement a method to get classes
                Staff = GetStaff(), // Implement a method to get staff members
                TrainingSessions = GetTrainingSessions() // Implement a method to get training sessions
            };

            return View(viewModel);
        }

        public List<Class> GetClasses()
        {
            // This method should return a list of classes from your database
            // For example:
            return dbContext.Classes.ToList();
        }

        public List<Register> GetStaff()
        {
            // This method should return a list of staff members from your database
            // For example:
            return dbContext.Users.Where(u => u.IsGymStaff).ToList();
        }

        public List<TrainingSession> GetTrainingSessions()
        {
            // This method should return a list of training sessions from your database
            // For example:
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


        // GET: ManageTrainingSessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ShowRegisterLoginLinks = true;
            TrainingSession trainingSession = dbContext.TrainingSessions.Find(id);
            if (trainingSession == null)
            {
                return HttpNotFound();
            }
            return View(trainingSession);
        }

        // POST: ManageTrainingSessions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClassID,StaffID,DateTime,Capacity")] TrainingSession trainingSession)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(trainingSession).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainingSession);
        }

        // GET: ManageTrainingSessions/Delete/5
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

        // POST: ManageTrainingSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainingSession trainingSession = dbContext.TrainingSessions.Find(id);
            dbContext.TrainingSessions.Remove(trainingSession);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
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