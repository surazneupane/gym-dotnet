﻿using GymManagement.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GymManagement.Controllers
{
	public class RegistersController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		
		public ActionResult Create()
		{
			ViewBag.ShowRegisterLoginLinks = false;
			return View();
		}

		public ActionResult Login()
		{
			ViewBag.ShowRegisterLoginLinks = false;
			return View();
		}
		[HttpPost]
		public ActionResult Login(Models.Login login)
		{
			var user = db.Users.FirstOrDefault(x => x.UserName == login.Username);

			if (user != null && BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
			{
				TempData["Message"] = "Login Successfully";

				if (user.IsGymStaff)
				{
					return RedirectToAction("Dashboard", "Home");
				}
				else
				{
					return RedirectToAction("MemberDashboard", "Home", new { userId = user.UserId }); // Pass userId as route parameter
				}
			}
			else
			{
				TempData["Message"] = "Login Failed. Username or password is incorrect.";
				ViewBag.ShowRegisterLoginLinks = false;
				return View();
			}
		}


		public ActionResult Logout()
		{
			// Perform logout logic here (e.g., clearing authentication cookies, session, etc.)
			ViewBag.ShowRegisterLoginLinks = false;

			TempData["Message"] = "You have been logged out.";
			return RedirectToAction("Login");
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Register register, bool isGymStaff = false)
		{
			if (ModelState.IsValid)
			{
				var existingUser = db.Users.FirstOrDefault(x => x.UserName == register.UserName);
				if (existingUser != null)
				{
					TempData["Message"] = "User Already exists";
					ViewBag.ShowRegisterLoginLinks = false;
					return View();
				}

				// Hash the password using BCrypt
				string hashedPassword = BCrypt.Net.BCrypt.HashPassword(register.Password);
				register.Password = hashedPassword;

				register.UserId = Guid.NewGuid();

				if (isGymStaff)
				{
					register.Role = "GymStaff";
				}
				else
				{
					register.Role = "Member";
				}

				db.Users.Add(register);
				db.SaveChanges();

				TempData["Message"] = "Register Successfully";
				return RedirectToAction("Login");
			}

			return View(register);
		}




		// GET: Registers/Edit/5
		//public ActionResult Edit(Guid? id)
		//{
		//	if (id == null)
		//	{
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	Register register = db.Users.Find(id);
		//	if (register == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	return View(register);
		//}

		// POST: Registers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "Userid,UserName,Password,Email")] Register register)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		db.Entry(register).State = EntityState.Modified;
		//		db.SaveChanges();
		//		return RedirectToAction("Index");
		//	}
		//	return View(register);
		//}

		// GET: Registers/Delete/5
		//public ActionResult Delete(Guid? id)
		//{
		//	if (id == null)
		//	{
		//		return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//	}
		//	Register register = db.Users.Find(id);
		//	if (register == null)
		//	{
		//		return HttpNotFound();
		//	}
		//	return View(register);
		//}

		// POST: Registers/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public ActionResult DeleteConfirmed(Guid id)
		//{
		//	Register register = db.Users.Find(id);
		//	db.Users.Remove(register);
		//	db.SaveChanges();
		//	return RedirectToAction("Index");
		//}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
