﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreFront.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Details/5
        public ActionResult Search()
        {
            return View();
        }

        // GET: Home/Create
        public new ActionResult Profile()
        {
            return View();
        }

    }
}