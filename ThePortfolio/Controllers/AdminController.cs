﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThePortfolio.Models;

namespace ThePortfolio.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        private ThePortfolioContext db = new ThePortfolioContext();
        // GET: Admin
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //Post Index
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Email, string Password)
        {
            using (ThePortfolioContext dc = new ThePortfolioContext())
            {
                var v = dc.Users.Where(a => a.Email.Equals(Email)).FirstOrDefault();
               // PasswordManager pwdManager = new PasswordManager();
                //if (v != null && pwdManager.IsPasswordMatch(Password, v.Salt, v.Password))
                if(v!=null && v.Password == Password)
                {
                    FormsAuthentication.SetAuthCookie(v.Email, false);
                    // add user data in session sin order to use it on other places
                    Session["CurrentUser"] = v;
                    //Session["Username"] = v.Username.ToString();
                    Session["Avatar"] = v.Avatar.ToString();
                   // Session["LogedUserFullname"] = v.Name.ToString();
                    //Session["IsAdmin"] = v.IsAdmin;
                    //if (v.IsAdmin != AccessLevel.Registered)
                    //    return RedirectToAction("Index", "Moderator", new { id = v.ID });
                    //else
                        return RedirectToAction("EditUser", "Admin", new { id = v.ID });
                }
                else
                {
                    ViewBag.Message = "OOPS :(  You most likely forgot your email or Password!!!";
                }

            } // end using
            return View();
        }

        // GET: Users/Create
        [AllowAnonymous]
        public ActionResult CreateUser()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "ID,Name,LastName,StreetAddress,City,ZipCode,Country,Email,Password,BirthDate,PhoneNum,Facebook,Twitter,Linkedin,Avatar")] User user, HttpPostedFileBase Avatar)
        {
            if (Avatar != null)
            {
                string avatar = System.IO.Path.GetFileName(Avatar.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/Avatar"), avatar);
                // file is uploaded
                Avatar.SaveAs(path);

                user.Avatar = avatar;
            }

                if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "ID,Name,LastName,StreetAddress,City,ZipCode,Country,Email,Password,BirthDate,PhoneNum,Facebook,Twitter,Linkedin")] User user, HttpPostedFileBase Avatar)
        {
            if (Avatar != null)
            {
                string avatar = System.IO.Path.GetFileName(Avatar.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/Avatar"), avatar);
                // file is uploaded
                Avatar.SaveAs(path);

                user.Avatar = avatar;

                //Delete the old image
                var photoName = (string)Session["Avatar"];
                string fullPath = Request.MapPath("~/Images/Avatar/"
                + photoName);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

            }
            else
            {
               
                user.Avatar = (string)Session["Avatar"];
            }

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return View(user);
            }
            return View(user);
        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            var Message = Session["LogedUserFullname"] + " you are succesfully logged out.";
            Session.Clear();
            return RedirectToAction("Index", new { LogoutMessage = Message });
        }

        //Tags Methods
    }
}