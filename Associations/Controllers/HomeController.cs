using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Associations.Models;

namespace Associations.Controllers
{
    public class HomeController : Controller
    {
        private Db db = new Db();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
        public ActionResult Decks()
        {
            ViewBag.Title = "Decks Page";
            return View();
        }

    }
}
