using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using crud_adventureworks2019.Models;

namespace crud_adventureworks2019.Controllers
{
    public class HomeController : Controller
    {
        // Este index lo pase a Person, para que de una muestre los datos, pero lo dejo aqui por si se usa otra vez
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}