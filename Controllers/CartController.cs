using CoffeShop.Models;
using CoffeShop.Models.Database;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult oneProductCart(int idproduct)
        {
            if (Session["Client"] != null)
            {
                int idClient = (int)Session["Client"];
                insertData insert = new insertData();
                bool check = insert.insertCart(idproduct, 1, idClient);
                if (check == true)
                {
                    TempData["Message"] = "Success";
                }
                else
                {
                    TempData["Message"] = "Error";
                }
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult multiProductCart(CartView cv)
        {
            if (Session["Client"] != null)
            {
                int idClient = (int)Session["Client"];
                insertData insert = new insertData();
                bool check = insert.insertCart((int)cv.idproduct, (int)cv.amount, idClient);
                if (check == true)
                {
                    TempData["Success"] = "Success";
                    return RedirectToAction("ProductDetail", "Product", new { idproduct = cv.idproduct });
                }
                else
                {
                    TempData["Success"] = "Error";
                    return RedirectToAction("ProductDetail", "Product", new { idproduct = cv.idproduct });
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult UserCart()
        {
            if (Session["Client"] != null)
            {
                int idClient = (int)Session["Client"];
                getData gt = new getData();
                ViewBag.lsCart = gt.getUserCart(idClient);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult updateCart(int id,int amount)
        {
            if (Session["Client"]!=null)
            {
                CartView cv = new CartView();
                cv.id = id;
                cv.amount = amount;
                updateData ud = new updateData();
                int idClient = (int) Session["Client"];
                bool check = ud.updateCart(cv,idClient);
                if (check == true)
                {
                    return RedirectToAction("PartialCart", "Cart");
                } else
                {
                    TempData["Error"] = "There are some error please check back";
                    return RedirectToAction("UserCart", "Cart");
                }
            } else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult removeCart(int idCart)
        {
            if (Session["Client"]!=null)
            {
                updateData ud = new updateData();
                bool check = ud.removeCart(idCart);
                if (check == true)
                {
                    return RedirectToAction("PartialCart", "Cart");
                }
                else
                {
                    TempData["Error"] = "There are some error please check back";
                    return RedirectToAction("UserCart", "Cart");
                }
            } else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult PartialCart()
        {
            int idClient = (int)Session["Client"];
            getData gt = new getData();
            ViewBag.lsCart = gt.getUserCart(idClient);
            return View();
        }


    }
}