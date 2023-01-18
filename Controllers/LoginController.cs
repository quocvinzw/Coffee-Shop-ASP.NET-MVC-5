using CoffeShop.Models;
using CoffeShop.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CoffeShop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (Session["Client"] == null || Response.Cookies["Emp"] == null || Response.Cookies["Admin"] == null)
            {
                return View();
            } else
            {
                Server.TransferRequest("/");
                return RedirectToAction("Index","WebPage");
            }
        }

        public void checkLogin(string email, string password)
        {
            getData gt = new getData();
            string type = gt.checkUserAccount(email, password);
            switch (type)
            {
                case "Admin":
                    cookieAdmin();
                    //return RedirectToAction("Index", "Admin");
                    break;
                case "Client":
                    ViewData["Login"] = "Success";
                    sessionClient(gt.getIDClient(email, password));
                    Server.TransferRequest("/");
                    break;
                case "Emp":
                    EmpView ev = gt.getEmpInfo(email, password);
                    switch (ev.pos)
                    {
                        case "Service Staff":
                            cookieEmp("Service Staff");
                            //return RedirectToAction("Index","Service");
                            Server.TransferRequest("/Service");
                            break;
                        case "Marketing":
                            cookieEmp("Marketing");
                            //return RedirectToAction("Index", "Marketing");
                            Server.TransferRequest("/Marketing");
                            break;
                        case "Director":
                            cookieEmp("Director");
                            //return RedirectToAction("Index","Director");
                            Server.TransferRequest("Director");
                            break;
                    }
                    TempData["Error"] = "Error";
                    //return RedirectToAction("Index", "Login");
                    Server.TransferRequest("/Login");
                    break;
                default:
                    TempData["Error"] = "Error";
                    //return RedirectToAction("Index", "Login");
                    Server.TransferRequest("/Login");
                    break;
            }
        }

        public void Login()
        {
            Server.TransferRequest("/Login");
        }

        private void sessionClient(int idUser)
        {
            Session["Client"] = idUser;
        }

        private void cookieAdmin()
        {
            HttpCookie ck = new HttpCookie("Admin", "Admin");
            ck.Expires.AddHours(8);
            Response.Cookies.Add(ck);
        }

        private void cookieEmp(string Pos)
        {
            HttpCookie ck = new HttpCookie(Pos, Pos);
            ck.Expires.AddHours(8);
            Response.Cookies.Add(ck);
        }

    }
}