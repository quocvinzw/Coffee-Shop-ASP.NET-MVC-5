using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeShop.Controllers
{
    public class LogOutController : Controller
    {
        // GET: LogOut
        public ActionResult Index()
        {
            bool Error = false;
            if (Error == false)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }

        public void startLogOut()
        {
            if (Session["Client"] != null)
            {
                Session.Remove("Client");
                TempData["LogOut"] = "LogOut";
                Server.TransferRequest("/");
            }           
            if (Request.Cookies["Marketing"]!=null)
            {
                Response.Cookies["Marketing"].Expires = DateTime.Now.AddYears(-1);
                TempData["LogOut"] = "LogOut";
                Server.TransferRequest("/");
            }
            if (Request.Cookies["Director"] != null)
            {
                Response.Cookies["Director"].Expires = DateTime.Now.AddYears(-1);
                TempData["LogOut"] = "LogOut";
                Server.TransferRequest("/");
            }
            if (Request.Cookies["Service Staff"] != null)
            {
                Response.Cookies["Service Staff"].Expires = DateTime.Now.AddYears(-1);
                TempData["LogOut"] = "LogOut";
                Server.TransferRequest("/");
            }
            if (Request.Cookies["Admin"]!=null)
            {
                Response.Cookies["Admin"].Expires = DateTime.Now.AddDays(-1D);
                TempData["LogOut"] = "LogOut";
                Server.TransferRequest("/");
            }
            Server.TransferRequest("/Login");

        }

    }
}