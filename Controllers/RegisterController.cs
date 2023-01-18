using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeShop.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            if( Session["Client"] == null || Response.Cookies["Emp"] == null || Response.Cookies["Admin"] == null)
            {
                return View();
            } else
            {
                return RedirectToAction("Index","WebPage");
            }
        }

        [HttpPost]
        public ActionResult Index(ClientView cw)
        {
            if (ModelState.IsValid)
            {
                Boolean Gender = false;
                CoffeShopEntities en = new CoffeShopEntities();
                if (cw.gender == 1)
                {
                    Gender = true;
                }
                en.Clients.Add(new Client
                {
                    name = cw.name,
                    password = cw.password,
                    birth=cw.birth,
                    email=cw.email,
                    gender=Gender,
                    phone=cw.phone,
                    status=cw.status,
                });
                en.SaveChanges();
                TempData["Register"] = "123";
                return RedirectToAction("Index", "Login");
            } else
            {
                ModelState.AddModelError("", "Error for Create Account Please Check Back");
            }  
            return View(cw);
        }

       
    }
}