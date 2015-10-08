using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThePortfolio.Models;

namespace ThePortfolio.Controllers
{
    [Authorize]
    public class ValuesController : Controller
    {
        private ThePortfolioContext db = new ThePortfolioContext();

        // GET: Values
        public ActionResult Index()
        {
            return View(db.Values.ToList());
        }

        // GET: Values/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = db.Values.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            return View(value);
        }

        // GET: Values/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Values/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description")] Value value)
        {
            if (ModelState.IsValid)
            {
                db.Values.Add(value);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(value);
        }

        // GET: Values/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = db.Values.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            return View(value);
        }

        // POST: Values/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description")] Value value)
        {
            if (ModelState.IsValid)
            {
                db.Entry(value).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(value);
        }

        // GET: Values/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = db.Values.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            return View(value);
        }

        // POST: Values/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Value value = db.Values.Find(id);
            db.Values.Remove(value);
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
