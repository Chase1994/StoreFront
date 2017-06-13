using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.Models;
using System.Data.Entity.Validation;
using System.Web.Security;

namespace StoreFront.Controllers
{
    public class UserController : Controller
    {
        //Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //Registration POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude= "DateModified, ModifiedBy")]Users user)
        {
            bool Status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {
                var exists = doesUserNameExist(user.UserName);
                if (exists)
                {
                    ModelState.AddModelError("UserNameExist", "User Name is already in use. Try another.");
                    return View(user);
                }

                user.Password = Crypto.HashPassword(user.Password);
                user.IsAdmin = false;
                user.DateCreated = System.DateTime.Now;
                user.CreatedBy = user.UserName;
                user.ConfirmPassword = user.Password;

                using (StoreFrontDBEntities dc = new StoreFrontDBEntities())
                {
                    //for debugging purposes (Entity Validation Exceptions)
                    dc.Users.Add(user);
                    try
                    {
                        dc.SaveChanges();
                        message = " You have successfully registered your account. Well done, that took a lot of skill. " +
                            " You should be proud of yourself!";
                        Status = true;
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message1 = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting
                                // the current instance as InnerException
                                raise = new InvalidOperationException(message1, raise);
                            }
                        }
                        throw raise;
                    }
                }

            }
            else
            {
                message = " Ya done goofed, check your inputs again mate.";
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
                var v = dc.Users.Where(a => a.UserName == login.UserName).FirstOrDefault();
                if (v != null)
                {
                    if (Crypto.ValidatePassword(login.Password, v.Password))
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
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool doesUserNameExist(string userName)
        {
            using (StoreFrontDBEntities dc = new StoreFrontDBEntities())
            {
                var v = dc.Users.Where(a => a.UserName == userName).FirstOrDefault();
                return v != null;
            }
        }

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
        [HttpPost]
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