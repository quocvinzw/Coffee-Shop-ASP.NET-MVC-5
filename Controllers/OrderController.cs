using CoffeShop.Models;
using CoffeShop.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeShop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            getData gt = new getData();
            ViewBag.lsOrder = gt.getOrderDefault();
            ViewBag.lsSale = gt.getOrderSale();
            return View();
        }

        public ActionResult OrderDetail(int idOrder)
        {
            getData gt = new getData();
            ViewBag.lsOrder = gt.orderDetailID(idOrder);
            return View();
        }

        public void oneOrderClient(int idProduct)
        {
            if (Session["Client"] != null)
            {
                int idClient = (int)Session["Client"];
                insertData insert = new insertData();
                bool check = insert.insertOrder(idProduct, 1, idClient);
                if (check == true)
                {
                    TempData["Message"] = "Success";
                   Server.TransferRequest("/Order");
                }
                else
                {
                    TempData["Message"] = "Error";
                    Server.TransferRequest("/Order");
                }
            }
            else
            {
                Server.TransferRequest("/Login");
            }
        }

        public ActionResult multiOrderClient(OrderView or)
        {
            if (Session["Client"] != null)
            {
                insertData insert = new insertData();
                int idClient = (int)Session["Client"];
                bool check = insert.insertOrder((int)or.idproduct, (int)or.amount, idClient);
                if (check == true)
                {
                    TempData["Success"] = "Success";
                    return RedirectToAction("OrderDetail", "Order", new { idOrder = or.idproduct });
                }
                else
                {
                    TempData["Success"] = "Error";
                    return RedirectToAction("OrderDetail", "Order", new { idOrder = or.idproduct });
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult UserOrder()
        {
            if (Session["Client"] != null)
            {
                int idClient = (int)Session["Client"];
                getData gt = new getData();
                ViewBag.lsOrder = gt.getUserOrder(idClient);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult updateOrder(int id,int amount)
        {
            if (Session["Client"] != null)
            {
                OrderView cv = new OrderView();
                cv.id= id;
                cv.amount= amount;
                updateData ud = new updateData();
                int idClient = (int)Session["Client"];
                bool check = ud.updateOrder(cv, idClient);
                if (check == true)
                {
                    return RedirectToAction("PartOrder", "Order");
                }
                else
                {
                    TempData["Error"] = "There are some error please check back";
                    return RedirectToAction("UserOrder", "Order");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult removeOrder(int idOrder)
        {
            if (Session["Client"] != null)
            {
                updateData ud = new updateData();
                bool check = ud.removeOrder(idOrder);
                if (check == true)
                {
                    return RedirectToAction("PartOrder", "Order");
                }
                else
                {
                    TempData["Error"] = "There are some error please check back";
                    return RedirectToAction("UserOrder", "Order");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult PartOrder()
        {
            int idClient = (int)Session["Client"];
            getData gt = new getData();
            ViewBag.lsOrder = gt.getUserOrder(idClient);
            return View();
        }
    }
}