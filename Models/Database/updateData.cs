using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeShop.Models.Database
{
    public class updateData
    {
        CoffeShopEntities cs;
        public updateData()
        {
            cs = new CoffeShopEntities();
        }
        #region Admin
        public void changeStatusEmp(int ID)
        {
            var emp = (from e in cs.Emps where e.id == ID select e).FirstOrDefault();
            if (emp != null)
            {
                switch (emp.status)
                {
                    case true: emp.status = false; break;
                    case false: emp.status = true; break;
                    default:
                        break;
                }
                cs.SaveChanges();
            }
        }

        public void changeStatusClient(int idClient)
        {
            var client = (from c in cs.Clients where c.id == idClient select c).FirstOrDefault();
            if (client != null)
            {
                switch (client.status)
                {
                    case true: client.status = false; break;
                    case false: client.status = true; break;
                    default:
                        break;
                }
            }
            cs.SaveChanges();
        }
        #endregion

        #region Marketing
        public bool changeStatusCate(int idCate)
        {
            bool check = true;
            try
            {
                var cate = (from c in cs.Categories where c.id == idCate select c).FirstOrDefault();
                if (cate != null)
                {
                    if (cate.status == true)
                    {
                        cate.status = false;
                    }
                    else
                    {
                        cate.status = true;
                    }
                }
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool changeStatusVoucher(int idVoucher)
        {
            bool check = true;
            try
            {
                var voucher = (from v in cs.Vouchers where v.id == idVoucher select v).FirstOrDefault();
                if (voucher != null)
                {
                    switch (voucher.status)
                    {
                        case true: voucher.status = false; break;
                        case false: voucher.status = true; break;
                    }
                }
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool changeStatusProduct(int idProduct)
        {
            bool check = true;
            try
            {
                var product = (from p in cs.Products where p.id == idProduct select p).FirstOrDefault();
                switch (product.status)
                {
                    case true: product.status = false; break;
                    case false: product.status = true; break;
                }
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool editCategory(CategoryView cv)
        {
            bool check = true;
            try
            {
                var editCate = (from c in cs.Categories where c.id == cv.id select c).FirstOrDefault();
                editCate.name = cv.name;
                editCate.status = cv.status;
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool editVoucher(VoucherView vv)
        {
            bool check = true;
            try
            {
                var voucher = (from v in cs.Vouchers where v.id == vv.id select v).FirstOrDefault();
                voucher.name = vv.name;
                voucher.discount = vv.discount;
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool editProduct(ProductView pv)
        {
            bool check = true;
            try
            {
                var product = (from p in cs.Products
                               join c in cs.Categories on p.category equals c.id
                               where p.id == pv.id
                               select p
                               ).FirstOrDefault();
                product.name = pv.name;
                product.price = pv.price;
                product.description = pv.description;
                product.category = pv.category;
                if (pv.image != null)
                {
                    string newimage = ActionImage.uploadFile(pv.image);
                    product.image = newimage;
                }
                else
                {
                    product.image = pv.imagename;
                }
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool changeStatusVoucherDetail(int idvd)
        {
            bool check = true;
            try
            {
                var getVoucherDetail = cs.VoucherDetails.Where(vd => vd.id == idvd).FirstOrDefault();
                var checkProduct = cs.VoucherDetails.Any(vd => vd.idproduct == getVoucherDetail.idproduct && vd.status == true);
                var checkStatus = cs.VoucherDetails.Any(vd => vd.id == idvd && vd.status == true);
                if (checkProduct == true && checkStatus == false)
                {
                    check = false;
                }
                else
                {
                    var voucherDetail = (from vd in cs.VoucherDetails where vd.id == idvd select vd).FirstOrDefault();
                    switch (voucherDetail.status)
                    {
                        case true: voucherDetail.status = false; break;
                        case false: voucherDetail.status = true; break;
                    }
                    cs.SaveChanges();
                }
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }
        #endregion

        #region Cart
        public bool updateCart(int id, int amount, int idClient)
        {
            getData gt = new getData();
            bool check = true;
            try
            {
                var selectCart = (from c in cs.Carts where c.id == id && c.status == false && c.idclient == idClient select c).SingleOrDefault();
                selectCart.amount = selectCart.amount + amount;
                selectCart.price = selectCart.price + gt.getPriceProduct((int)selectCart.idproduct) * amount;
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool updateCart(CartView cv, int idClient)
        {
            bool check = true;
            getData gt = new getData();
            try
            {
                var selectCart = (from c in cs.Carts where c.id == cv.id && c.status == false && c.idclient == idClient select c).SingleOrDefault();
                selectCart.amount = cv.amount;
                selectCart.price = gt.getPriceProduct((int)selectCart.idproduct) * cv.amount;
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool removeCart(int idCart)
        {
            bool check = true;
            try
            {
                var cart = cs.Carts.Where(c => c.id == idCart).SingleOrDefault();
                cs.Carts.Remove(cart);
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }
        #endregion

        #region Order
        public bool updateOrder(int id, int amount, int idClient)
        {
            getData gt = new getData();
            bool check = true;
            try
            {
                var selectOrder = (from c in cs.ClientOrders where c.id == id && c.status == "0" && c.idclient == idClient select c).SingleOrDefault();
                selectOrder.amount = selectOrder.amount + amount;
                selectOrder.price = selectOrder.price + gt.getPriceProduct((int)selectOrder.idproduct) * amount;
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool updateOrder(OrderView cv, int idClient)
        {
            bool check = true;
            getData gt = new getData();
            try
            {
                var selectCart = (from o in cs.ClientOrders where o.id == cv.id && o.status != "2" && o.idclient == idClient select o).SingleOrDefault();
                selectCart.amount = cv.amount;
                selectCart.price = gt.getPriceProduct((int)selectCart.idproduct) * cv.amount;
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool removeOrder(int idOrder)
        {
            bool check = true;
            try
            {
                var order = cs.ClientOrders.Where(o => o.id == idOrder).SingleOrDefault();
                cs.ClientOrders.Remove(order);
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }
        #endregion

        public bool changeStatusOrder(int idOrder)
        {
            bool check = true;
            try
            {
                var Order = (from o in cs.ClientOrders where o.id == idOrder select o).SingleOrDefault();
                switch (Order.status)
                {
                    case "0": Order.status = "1"; break;
                    case "1": Order.status = "2"; break;
                }
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

    }

}