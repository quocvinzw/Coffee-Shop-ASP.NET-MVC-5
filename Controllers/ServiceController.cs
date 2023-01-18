using CoffeShop.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeShop.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            if (Request.Cookies["Service Staff"]!=null)
            {
                return View();
            } else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult ClientOrder()
        {
            if (Request.Cookies["Service Staff"]!=null)
            {
                getData gt = new getData();
                ViewBag.lsOrder = gt.getClientOrder();
                return View();
            } else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        
        public ActionResult changeStatusOrder(int idOrder)
        {
            updateData ud = new updateData();
            bool check = ud.changeStatusOrder(idOrder);
            if (check == true)
            {
                TempData["AlertSuccess"] = "";
            }
            return RedirectToAction("ClientOrder", "Service");
        }
    }
}