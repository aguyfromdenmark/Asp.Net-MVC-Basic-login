using LoginExercise.Context;
using LoginExercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LoginExercise.Controllers
{
    public class UserController : Controller
    {
        LoginContext db = new LoginContext();
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var hashedPassword = (string)FormsAuthentication.HashPasswordForStoringInConfigFile(password + username, "SHA1");
            var loginUser = db.Users.Where(x => x.Username == username).SingleOrDefault();

            if (loginUser != null)
            {
                if (DateTime.Compare(loginUser.LoginTimeout, DateTime.Now) > 0)
                {
                    TimeSpan waitTime = loginUser.LoginTimeout - DateTime.Now;
                    var message = "Too many tries! Please wait " + waitTime.TotalMinutes.ToString("0.0") + " minutes :) ";
                    ViewBag.Message = message;
                    return View();
                }
                else
                {
                    if (loginUser.Password == hashedPassword)
                    {
                        loginUser.LoginTimeout = DateTime.Now;
                        loginUser.LoginTries = 0;
                        System.IO.File.AppendAllText(Server.MapPath("~/loginLog.txt"), "Logged in: \"" + loginUser.Username + "\" - " + DateTime.Now.ToString());
                        Session["loggedIn"] = true;
                        return RedirectToAction("Secret", "Home");
                    }
                    else
                    {
                        Session["loggedIn"] = false;
                        loginUser.LoginTries++;

                        if (loginUser.LoginTries >= 3)
                        {
                            loginUser.LoginTimeout = DateTime.Now.AddMinutes(15);
                        }

                        db.SaveChanges();
                        ViewBag.Message = "Invalid login";
                        return View();
                    }
                }
            }
            ViewBag.Message = "No user was found. Sorry.....";
            return View();
        }

        public ActionResult LogOut()
        {
            Session["loggedIn"] = false;
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string username, string password)
        {
            var newUser = new UserModel();

            var userAlreadyCreated = db.Users.Where(x => x.Username == username).SingleOrDefault();
            if (userAlreadyCreated == null)
            {
                newUser.Id = Guid.NewGuid();
                newUser.Username = username;
                newUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password + username, "SHA1");

                db.Users.Add(newUser);
                db.SaveChanges();

                ViewBag.Message = "Account was created!";

                return View();
            }else
            {
                ViewBag.Message = "Username is already taken...";
                return View();
            }
            
        }
    }
}