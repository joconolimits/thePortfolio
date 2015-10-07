using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThePortfolio.Models;
using ThePortfolio.ViewModels;

namespace ThePortfolio.Controllers
{
    public class PortfolioController : Controller
    {
        private ThePortfolioContext db = new ThePortfolioContext();
        // GET: Portfolio
        public ActionResult Index()
        {
            //int id = 1;
            FrontEnd FrontEndData = new FrontEnd();
            FrontEndData.User = db.Users.FirstOrDefault();
            FrontEndData.Skills = db.Skills.ToList();
            FrontEndData.Tags = db.Tags.ToList();
            FrontEndData.portfolioItems = db.PortfolioItems.Include("Tags").ToList();
            FrontEndData.JournalItems = db.JournalItems.ToList();
            FrontEndData.Services = db.Services.ToList();
            return View(FrontEndData);
        }
    }
}