using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            FrontEndData.Slides = db.Slides.ToList();
            FrontEndData.Values = db.Values.ToList();
            return View(FrontEndData);
        }

       
        [HttpPost]
        public ActionResult ContactUs(string name, string email, string message)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
            {
                return RedirectToAction("Index");
            }
            else
            {
                var mailMessage = new MailMessage(email, "joconolimits@gmail.com")
                {
                    Subject = "Contact Form",
                    Body = message + string.Format(" <br/><br/><br/>By: {0}  {1}", name, email)
                };
                Client(mailMessage);
                ViewBag.Message = "The email was sent successfully!"; 
                return RedirectToAction("Index");
            }

        
    }

        public void Client(MailMessage message)
        {
            message.IsBodyHtml = true;
            var client = new SmtpClient();
            var credential = new NetworkCredential
            {
                UserName = "blindcarrots1@gmail.com",
                Password = "ticketingSystem"
            };
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.Credentials = credential;
            client.EnableSsl = true;
            client.Send(message);
        }
    }
}