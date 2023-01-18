using CoffeShop.Models;
using CoffeShop.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Request.Cookies["Admin"]!=null)
            {
                return View();
            } else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult CreateEmp()
        {
            if (Request.Cookies["Admin"]!=null)
            {
                getData gt = new getData();
                ViewBag.lsBranch = gt.getBranch();
                return View();
            } else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult CreateEmp(EmpView ew)
        {
            if (ModelState.IsValid)
            {
                if (ew.gender == 0)
                {
                    ew.genderSelect = true;
                }
                else 
                { 
                    ew.genderSelect = false;
                }
                insertData insert = new insertData();
                bool Check =insert.insertEmp(ew);
                EmpView erset = new EmpView();
                if (Check == true)                
                {
                    TempData["Alert"] = "Success";
                    return RedirectToAction("CreateEmp");
                } 
                else
                {
                    TempData["Alert"] = "Error";
                    return RedirectToAction("CreateEmp");
                }
            }
            else
            {
                getData gt = new getData();
                ViewBag.lsBranch = gt.getBranch();
                TempData["Alert"] = "Error";
                return View(ew);
            }
        }

        public ActionResult ManageEmp()
        {
            if (Request.Cookies["Admin"]!=null)
            {
                getData gd = new getData();
                List<EmpView> ls = gd.getEmp();
                ViewBag.lsEmp = ls;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult UpdateStatus(int ID)
        {
            updateData ud = new updateData();
            ud.changeStatusEmp(ID);
            TempData["AlertSuccess"]="Success";
            return RedirectToAction("ManageEmp", "Admin");
        }

        public ActionResult ManageClient()
        {
            if (Request.Cookies["Admin"]!=null)
            {
                getData gt = new getData();
                ViewBag.lsClient=gt.getClient();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult accountClient(int idClient)
        {
            updateData ud = new updateData();
            ud.changeStatusClient(idClient);
            TempData["AlertSuccess"] = "Success";
            return RedirectToAction("ManageClient", "Admin");
        }
    }
}