using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity.Infrastructure;
using StoreFront.Data;

namespace StoreFront.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontDataEntities db = new StoreFrontDataEntities();

        
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //viewbag variables to handle sorting of products
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            //if the searchstring has something in it, set the page back to 1
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //set CurrentFilter to whatever the searchstring is
            ViewBag.CurrentFilter = searchString;

            //grab products in the database
            var product = from p in db.Products
                           select p;

            //if the searchString exists, grab all products that contain the string
            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(p => p.ProductName.Contains(searchString));
            }

            //handles sorting for ascending and descending based on price and name
            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(p => p.ProductName);
                    break;
                case "Price":
                    product = product.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(p => p.Price);
                    break;
                default: //name ascending
                    product = product.OrderBy(p => p.ProductName);
                    break;
            }

            //paging set to 10
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
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
