using CoffeShop.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            getData gt = new getData();
            ViewBag.lsPro=gt.getProductDefault();
            ViewBag.lsSale=gt.getProductSale();
            return View();
        }

        public ActionResult ProductDetail(int idproduct)
        {
            getData gt = new getData();
            ViewBag.Product = gt.productDetailID(idproduct);
            return View();
        }
    }
}