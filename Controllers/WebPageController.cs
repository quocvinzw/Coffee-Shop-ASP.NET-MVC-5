using CoffeShop.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeShop.Controllers
{
    public class WebPageController : Controller
    {
        // GET: WebPage
        public ActionResult Index()
        {
            getData gt = new getData();
            ViewBag.lsMenu = gt.getMenu();
            return View();
        }
    }
}