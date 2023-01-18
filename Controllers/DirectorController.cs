using CoffeShop.Models.Database;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeShop.Controllers
{
    public class DirectorController : Controller
    {
        // GET: Director
        public ActionResult Index()
        {
            if (Request.Cookies["Director"]!=null)
            {
                return View();
            } else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult ManageEmp()
        {
            if (Request.Cookies["Director"] != null)
            {
                getData gd = new getData();
                List<EmpView> ls = gd.getEmp();
                ViewBag.lsEmp = ls;
                return View();
            } else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}