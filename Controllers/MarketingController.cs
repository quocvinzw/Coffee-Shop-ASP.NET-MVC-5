using CoffeShop.Models;
using CoffeShop.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeShop.Controllers
{
    public class MarketingController : Controller
    {
        // GET: Marketing
        public ActionResult Index()
        {

            return View();

        }

        #region Product
        public ActionResult CreateProduct()
        {

            getData gt = new getData();
            ViewBag.lsCate = gt.getCategory();
            return View();

        }

        [HttpPost]
        public ActionResult CreateProduct(ProductView pv)
        {
            if (ModelState.IsValid)
            {
                insertData insert = new insertData();
                bool check = insert.insertProduct(pv);
                if (check == true)
                {
                    TempData["Alert"] = "Success";
                }
                else
                {
                    TempData["Alert"] = "Error";
                }
                return RedirectToAction("CreateProduct", "Marketing");
            }
            else
            {
                return View(pv);
            }
        }
        #endregion

        #region Category
        public ActionResult CreateCategory()
        {

            return View();


        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryView cw)
        {
            insertData insert = new insertData();
            bool check = insert.insertCategory(cw);
            if (check == true)
            {
                TempData["Alert"] = "Success";
            }
            else
            {
                TempData["Alert"] = "Error";
            }
            return RedirectToAction("CreateCategory", "Marketing");
        }
        #endregion

        #region Voucher
        public ActionResult CreateVoucher()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateVoucher(VoucherView vv)
        {
            if (ModelState.IsValid)
            {
                insertData insert = new insertData();
                bool check = insert.insertVoucher(vv);
                if (check == true)
                {
                    TempData["Alert"] = "Success";
                }
                else
                {
                    TempData["Alert"] = "Error";
                }
                return RedirectToAction("CreateVoucher", "Marketing");
            }
            else
            {
                return View(vv);
            }
        }

        #endregion

        #region Manage
        public ActionResult ManageCategory()
        {

            getData gt = new getData();
            ViewBag.lsCategory = gt.getCategory();
            return View();

        }

        public ActionResult ManageVoucher()
        {

            getData gt = new getData();
            ViewBag.lsVoucher = gt.getVoucher();
            return View();


        }

        public ActionResult ManageProduct()
        {

            getData gt = new getData();
            ViewBag.lsProduct = gt.getProduct();
            return View();

        }
        #endregion

        #region Update
        public ActionResult changeStatusCate(int idcate)
        {
            updateData ud = new updateData();
            bool check = ud.changeStatusCate(idcate);
            if (check == true)
            {
                TempData["Alert"] = "Success";
            }
            else
            {
                TempData["Alert"] = "Error";
            }
            return RedirectToAction("ManageCategory", "Marketing");
        }

        public ActionResult changeStatusVoucher(int idvoucher)
        {
            updateData ud = new updateData();
            bool check = ud.changeStatusVoucher(idvoucher);
            if (check == true)
            {
                TempData["Alert"] = "Success";
            }
            else
            {
                TempData["Alert"] = "Error";
            }
            return RedirectToAction("ManageVoucher", "Marketing");
        }

        public ActionResult changeStatusProduct(int idproduct)
        {
            updateData ud = new updateData();
            bool check = ud.changeStatusProduct(idproduct);
            if (check == true)
            {
                TempData["Alert"] = "Success";
            }
            else
            {
                TempData["Alert"] = "Error";
            }
            return RedirectToAction("ManageProduct", "Marketing");
        }
        #endregion

        #region Edit
        public ActionResult EditCategory(int idcate)
        {

            getData gt = new getData();
            CategoryView cv = gt.getCatebyID(idcate);
            return View(cv);


        }

        [HttpPost]
        public ActionResult EditCategory(CategoryView cv)
        {
            if (ModelState.IsValid)
            {
                updateData ud = new updateData();
                bool check = ud.editCategory(cv);
                if (check == true)
                {
                    TempData["Edit"] = "Success";
                }
                else
                {
                    TempData["Alert"] += "Error";
                }
                return RedirectToAction("ManageCategory", "Marketing");
            }
            else
            {
                return View(cv);
            }
        }

        public ActionResult EditVoucher(int idvoucher)
        {

            getData gt = new getData();
            VoucherView vv = gt.getVoucherbyID(idvoucher);
            return View(vv);

        }

        [HttpPost]
        public ActionResult EditVoucher(VoucherView cv)
        {
            if (ModelState.IsValid)
            {
                updateData ud = new updateData();
                bool check = ud.editVoucher(cv);
                if (check == true)
                {
                    TempData["Edit"] = "Success";
                }
                else
                {
                    TempData["Alert"] = "Error";
                }
                return RedirectToAction("ManageVoucher", "Marketing");
            }
            else
            {
                return View(cv);
            }
        }

        public ActionResult EditProduct(int idproduct)
        {

            getData gt = new getData();
            ViewBag.lsCate = gt.getCategory();
            ProductView pv = gt.getProductbyID(idproduct);
            return View(pv);

        }

        [HttpPost]
        public ActionResult EditProduct(ProductView pv)
        {
            if (ModelState.IsValid)
            {
                updateData ud = new updateData();
                bool check = ud.editProduct(pv);
                if (check == true)
                {
                    TempData["Edit"] = "Success";
                }
                else
                {
                    TempData["Alert"] = "Error";
                }
                return RedirectToAction("ManageProduct", "Marketing");
            }
            else
            {
                return RedirectToAction("errorEditProduct", "Marketing", pv);
            }
        }

        public ActionResult errorEditProduct(ProductView pv)
        {
            getData gt = new getData();
            ViewBag.lsCate = gt.getCategory();
            return View("EditProduct", pv);
        }
        #endregion

        #region AddVoucher
        public ActionResult AddVoucher()
        {

            getData gt = new getData();
            ViewBag.lsProduct = gt.vdProduct();
            ViewBag.lsVoucher = gt.vdVoucher();
            return View();


        }

        [HttpPost]
        public ActionResult AddVoucher(VoucherDetailView vd)
        {
            if (ModelState.IsValid)
            {
                insertData insert = new insertData();
                bool check = insert.insertVoucherDetails(vd);
                if (check == true)
                {
                    TempData["Alert"] = "Success";
                }
                else
                {
                    TempData["Alert"] = "Error";
                }
                return RedirectToAction("AddVoucher");
            }
            else
            {
                getData gt = new getData();
                ViewBag.lsProduct = gt.vdProduct();
                ViewBag.lsVoucher = gt.vdVoucher();
                return View(vd);
            }
        }

        public ActionResult ManageVoucherDetail()
        {

            getData gt = new getData();
            ViewBag.lsVoucherDetail = gt.getVoucherDetails();
            return View();

        }

        public ActionResult changeStatusVoucherDetail(int idvd)
        {
            updateData ud = new updateData();
            bool check = ud.changeStatusVoucherDetail(idvd);
            if (check == true)
            {
                TempData["Alert"] = "Success";
            }
            else
            {
                TempData["Alert"] = "Error";
            }
            return RedirectToAction("ManageVoucherDetail", "Marketing");
        }

        #endregion

    }
}