using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThePortfolio.Models;
using ThePortfolio.ViewModels;

namespace ThePortfolio.Controllers
{
    [Authorize]
    public class PortfolioItemsController : Controller
    {
        private ThePortfolioContext db = new ThePortfolioContext();

        // GET: PortfolioItems
        public ActionResult Index()
        {
            var AllTags = db.Tags.ToList();

            var portfolioItems = db.PortfolioItems.Include("Tags").ToList();
            var portfolioItemsViewModels = portfolioItems.Select(portfolioItem => portfolioItem.ToViewModel(AllTags)).ToList();
            return View(portfolioItemsViewModels);
        }

        //GET: PortfolioItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioItem portfolioItem = db.PortfolioItems.Find(id);
            if (portfolioItem == null)
            {
                return HttpNotFound();
            }
            return View(portfolioItem);
        }

        private ICollection<AssignedTags> PopulateTags()
        {
            var tags = db.Tags;
            var assignedTags = new List<AssignedTags>();

            foreach (var item in tags)
            {
                assignedTags.Add(new AssignedTags
                {
                    ID = item.ID,
                    Name = item.Name,
                    Assigned = false
                });
            }

            return assignedTags;
        }

        // GET: PortfolioItems/Create
        public ActionResult Create()
        {
            var portfolioItemViewModel = new PortfolioItemViewModel { Tags = PopulateTags()  };
            return View(portfolioItemViewModel);
        }

        // POST: PortfolioItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PortfolioItemViewModel portfolioItemViewModel, HttpPostedFileBase Image)
        {

            if (Image != null)
            {
                string ImageName = System.IO.Path.GetFileName(Image.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/PortfolioItems"), ImageName);
                // file is uploaded
                Image.SaveAs(path);

                portfolioItemViewModel.Image = ImageName;
            }

            if (ModelState.IsValid)
            {
                var portfolioItem = new PortfolioItem {
                    Title = portfolioItemViewModel.Title,
                    Description = portfolioItemViewModel.Description,
                    Image = portfolioItemViewModel.Image,
                    ProjcetUrl = portfolioItemViewModel.ProjcetUrl,
                };

                AddOrUpdateTags(portfolioItem, portfolioItemViewModel.Tags);
                db.PortfolioItems.Add(portfolioItem);;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(portfolioItemViewModel);
        }


        private void AddOrUpdateTags(PortfolioItem portfolioItem, IEnumerable<AssignedTags> assignedTags)
        {
            if (assignedTags == null) return;
            if (portfolioItem.ID != 0)
            {
                foreach (var tag in portfolioItem.Tags.ToList())
                {
                    portfolioItem.Tags.Remove(tag);
                }

                foreach (var tag in assignedTags.Where(c => c.Assigned))
                {
                    portfolioItem.Tags.Add(db.Tags.Find(tag.ID));
                }
            }
            else
            {
                // New user
                foreach (var assignedTag in assignedTags.Where(c => c.Assigned))
                {
                    var tag = new Tag { ID = assignedTag.ID };
                    db.Tags.Attach(tag);
                    portfolioItem.Tags.Add(tag);
                }
            }
        }


        // GET: PortfolioItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Get all tags
            var allDbTags = db.Tags.ToList();

            // Get the portfolioItem we are editing and include the tags already assigned to it
            var portfolioItem = db.PortfolioItems.Include("Tags").FirstOrDefault(x => x.ID == id);
            var portfolioItemViewModel = portfolioItem.ToViewModel(allDbTags);

            return View(portfolioItemViewModel);
        }

        // POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PortfolioItemViewModel portfolioItemViewModel, HttpPostedFileBase Image)
        {
            var photoName = db.PortfolioItems.Find(portfolioItemViewModel.ID).Image;
            if (Image != null)
            {
                string ImageName = System.IO.Path.GetFileName(Image.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/PortfolioItems"), ImageName);
                // file is uploaded
                Image.SaveAs(path);

                portfolioItemViewModel.Image = ImageName;

                //Delete the old image
                
                string fullPath = Request.MapPath("~/Images/PortfolioItems/"
                + photoName);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            else
            {

                portfolioItemViewModel.Image = photoName;
            }

            if (ModelState.IsValid)
            {
                var originalPortfolioItem = db.PortfolioItems.Find(portfolioItemViewModel.ID);
                AddOrUpdateTags(originalPortfolioItem, portfolioItemViewModel.Tags);
                db.Entry(originalPortfolioItem).CurrentValues.SetValues(portfolioItemViewModel);
                db.SaveChanges();

                //db.Entry(portfolioItem).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(portfolioItemViewModel);
        }

        // GET: PortfolioItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioItem portfolioItem = db.PortfolioItems.Find(id);
            if (portfolioItem == null)
            {
                return HttpNotFound();
            }
            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PortfolioItem portfolioItem = db.PortfolioItems.Find(id);
            db.PortfolioItems.Remove(portfolioItem);
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
