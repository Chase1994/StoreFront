using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.Models;

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
        public ActionResult Registration([Bind(Exclude= "IsAdmin, DateCreated, CreatedBy, DataModified, ModifiedBy")]User user)
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

                using (StoreFrontDBEntities dc = new StoreFrontDBEntities())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();
                }

            }
            else
            {
                message = " Invalid Request";
            }
            //UserName already exists

            //Password hashing

            //save data to database

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }
        //Login

        //Login POST

        //Logout
        [NonAction]
        public bool doesUserNameExist(string userName)
        {
            using (StoreFrontDBEntities dc = new StoreFrontDBEntities())
            {
                var v = dc.Users.Where(a => a.UserName == userName).FirstOrDefault();
                return v != null;
            }
        }
    }
}