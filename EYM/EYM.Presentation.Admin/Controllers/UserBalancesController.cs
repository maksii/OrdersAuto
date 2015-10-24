using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EYM.Entities;

namespace EYM.Presentation.Admin.Controllers
{
    public class UserBalancesController : Controller
    {
        private DinnerContext db = new DinnerContext();

        // GET: UserBalances
        public ActionResult Index()
        {
            var userBalances = db.UserBalances.Include(u => u.User);
            return View(userBalances.ToList());
        }

        // GET: UserBalances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBalance userBalance = db.UserBalances.Find(id);
            if (userBalance == null)
            {
                return HttpNotFound();
            }
            return View(userBalance);
        }

        // GET: UserBalances/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: UserBalances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserBalanceId,Credit,Date,Comment,UserId")] UserBalance userBalance)
        {
            if (ModelState.IsValid)
            {
                db.UserBalances.Add(userBalance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userBalance.UserId);
            return View(userBalance);
        }

        // GET: UserBalances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBalance userBalance = db.UserBalances.Find(id);
            if (userBalance == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userBalance.UserId);
            return View(userBalance);
        }

        // POST: UserBalances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserBalanceId,Credit,Date,Comment,UserId")] UserBalance userBalance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userBalance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userBalance.UserId);
            return View(userBalance);
        }

        // GET: UserBalances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBalance userBalance = db.UserBalances.Find(id);
            if (userBalance == null)
            {
                return HttpNotFound();
            }
            return View(userBalance);
        }

        // POST: UserBalances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserBalance userBalance = db.UserBalances.Find(id);
            db.UserBalances.Remove(userBalance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
