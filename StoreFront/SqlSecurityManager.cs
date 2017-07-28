using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront.Data;
using System.Web.SessionState;
namespace StoreFront
{
    public class SqlSecurityManager
    {
        StoreFrontDataEntities db = new StoreFrontDataEntities();

        public Boolean AuthenticateUser(string username, string password)
        {
            User theUser = db.Users.Where(u => u.UserName == username).FirstOrDefault();
            if (theUser != null && theUser.Password.Equals(password))
            {
                string name = theUser.UserName;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Session.Add("UserName", name);
                }
                return true;
            }
            else
            {
                return false;
            }   
        }

        public Boolean IsAdmin(string username)
        {
            string user;
            if (HttpContext.Current != null)
            {
                user = HttpContext.Current.Session["UserName"].ToString();
            }
            else
            {
                user = username;
            }

            User theUser = db.Users.Where(u => u.UserName == user).FirstOrDefault();
            if (Convert.ToBoolean(theUser.IsAdmin))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User LoadUser(string username)
        {
            User user = db.Users.Where(u => u.UserName == username).FirstOrDefault();
            return user;
        }
        
        public void SaveUser(User user)
        {
            //add user to database and save
            db.Users.Add(user);
            db.SaveChanges();
        }
        public void RegisterUser(User user)
        {
            //hash the user password for safe storage
            user.Password = Crypto.HashPassword(user.Password);
            user.ConfirmPassword = user.Password;
            user.IsAdmin = false;
            user.DateCreated = System.DateTime.Now;
            user.CreatedBy = user.UserName;

            //save user to database
            SaveUser(user);
        }
        public void DeleteUser(string username)
        {
            User user = db.Users.Where(u => u.UserName == username).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}