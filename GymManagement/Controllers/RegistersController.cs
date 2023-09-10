using GymManagement.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GymManagement.Controllers
{
	public class RegistersController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Registers
		//public ActionResult Index()
		//{
		//	return View(db.Users.ToList());
		//}

		//// GET: Registers/Details/5
		//public ActionResult Details(Guid? id)
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

		// GET: Registers/Create
		public ActionResult Create()
		{
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Login(Models.Login login)
		{
			var data = db.Users.Where(x => x.UserName == login.Username && x.Password == login.Password);
			if (data != null)
			{

				TempData["Message"] = "Login Successfully";
				return RedirectToAction("Index", "Home");
			}
			else
			{
				TempData["Message"] = "Login Failed username or password missing";
				return View();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Userid,UserName,Password,Email")] Register register)
		{
			if (ModelState.IsValid)
			{
				var data = db.Users.Where(x => x.UserName == register.UserName);
				if (data.Any())
				{
					TempData["Message"] = "User Already exists";
					return View();
				}

				// Hash the password using BCrypt
				string hashedPassword = BCrypt.Net.BCrypt.HashPassword(register.Password);
				register.Password = hashedPassword;

				register.Userid = Guid.NewGuid();
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
