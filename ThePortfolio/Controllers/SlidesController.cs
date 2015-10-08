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
    public class SlidesController : Controller
    {
        private ThePortfolioContext db = new ThePortfolioContext();

        // GET: Slides
        public ActionResult Index()
        {
            return View(db.Slides.ToList());
        }

        // GET: Slides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Slides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Image,Caption")] Slide slide, HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                string ImageName = System.IO.Path.GetFileName(Image.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/slides"), ImageName);
                // file is uploaded
                Image.SaveAs(path);

                slide.Image = ImageName;
            }
            if (ModelState.IsValid)
            {
                db.Slides.Add(slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slide);
        }

        // GET: Slides/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Image,Caption")] Slide slide, HttpPostedFileBase Image)
        {

            var photoName = db.Slides.Find(slide.ID).Image;
            if (Image != null)
            {
                string ImageName = System.IO.Path.GetFileName(Image.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/slides"), ImageName);
                // file is uploaded
                Image.SaveAs(path);

                slide.Image = ImageName;

                //Delete the old image

                string fullPath = Request.MapPath("~/Images/slides/"
                + photoName);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            else
            {

                slide.Image = photoName;
            }


            if (ModelState.IsValid)
            {
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slide);
        }

        // GET: Slides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slides.Find(id);

            string fullPath = Request.MapPath("~/Images/slides/"
               + slide.Image);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.Slides.Remove(slide);
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
