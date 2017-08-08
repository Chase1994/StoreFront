using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.Models;
using System.Data.Entity.Validation;
using System.Web.Security;
using StoreFront.Data;
namespace StoreFront.Controllers
{
    public class UserController : Controller
    {
        SqlSecurityManager sm = new SqlSecurityManager();
        StoreFrontDataEntities dc = new StoreFrontDataEntities();
        //Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //Registration POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude= "DateModified, ModifiedBy")]User user)
        {
            bool Status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {
                //check to see if the username exists in the database already
                var exists = doesUserNameExist(user.UserName);
                if (exists)
                {
                    //throw error if username already exists
                    ModelState.AddModelError("UserNameExist", "User Name is already in use. Try another.");
                    return View(user);
                }
                sm.RegisterUser(user);
            }
            else
            {
                message = "Error.";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }
        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl)
        {
            string message = "";
            using (StoreFrontDBEntities dc = new StoreFrontDBEntities())
            {
                //check if user is in database
                var user = dc.Users.Where(a => a.UserName == login.UserName).FirstOrDefault();
                if (user != null)
                {
                    if (Crypto.ValidatePassword(login.Password, user.Password))
                    {
                        int timeout = login.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.UserName, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Your username or password is unrecognized by the system.";
                    }
                }
                else
                {
                    message = "Your username or password is unrecognized by the system.";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        //Logout
        [Authorize]
        public ActionResult Logout()
        {
            //clear session and sign the user out of FormsAuthentication
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool doesUserNameExist(string userName)
        {
            //check if username exists in database already
            using (StoreFrontDBEntities dc = new StoreFrontDBEntities())
            {
                var v = dc.Users.Where(a => a.UserName == userName).FirstOrDefault();
                return v != null;
            }
        }

        //currently broken, need to update
        [Authorize]
        public ActionResult EditProfile()
        {
            StoreFrontDBEntities dc = new StoreFrontDBEntities();
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                Users user = dc.Users.FirstOrDefault(u => u.UserName.Equals(username));

                Users model = new Users();
                model.UserName = user.UserName;
                model.EmailAddress = user.EmailAddress;
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        //also broken, needs fixed
        [HttpPost]
        [Authorize]
        public ActionResult EditProfile(Users userprofile)
        {
            StoreFrontDBEntities dc = new StoreFrontDBEntities();
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;
                Users user = dc.Users.FirstOrDefault(u => u.UserName.Equals(username));
                user.UserName = userprofile.UserName;
                user.EmailAddress = userprofile.EmailAddress;

                dc.Entry(user).State = System.Data.Entity.EntityState.Modified;
                dc.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(userprofile);
        }
    }
}