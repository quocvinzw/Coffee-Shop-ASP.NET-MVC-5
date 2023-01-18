using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace CoffeShop.Models.Database
{
    public class insertData
    {
        private CoffeShopEntities cs;
        public insertData()
        {
            cs = new CoffeShopEntities();
        }

        #region Admin
        public bool insertEmp(EmpView ew)
        {
            bool check = true;
            try
            {
                cs.Emps.Add(new Emp
                {
                    birth = ew.birth,
                    branch = ew.branch,
                    email = ew.email,
                    gender = ew.genderSelect,
                    dwork = ew.dwork,
                    name = ew.name,
                    password = ew.password,
                    phone = ew.phone,
                    pos = ew.pos,
                    status = ew.status,
                });
                cs.SaveChanges();
            }
            catch
            {
                check = false;
            }
            return check;
        }
        #endregion

        #region Marketing
        public bool insertCategory(CategoryView cw)
        {
            bool check = true;
            try
            {
                cs.Categories.Add(new Category { name = cw.name, status = cw.status });
                cs.SaveChanges();
            }
            catch
            {
                check = false;
            }

            return check;
        }

        public bool insertProduct(ProductView pv)
        {
            bool check = true;
            try
            {
                string fileName = ActionImage.uploadFile(pv.image);
                cs.Products.Add(new Product
                {
                    name = pv.name,
                    image = fileName,
                    category = pv.category,
                    description = pv.description,
                    price = pv.price,
                    status = pv.status
                });
                cs.SaveChanges();
            }
            catch
            {
                check = false;
            }
            return check;
        }

        public bool insertVoucher(VoucherView vv)
        {
            bool check = true;
            try
            {
                cs.Vouchers.Add(new Voucher
                {
                    name = vv.name,
                    discount = vv.discount,
                    datecreate = DateTime.Now,
                    status = vv.status
                });
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }

            return check;
        }

        public bool insertVoucherDetails(VoucherDetailView vd)
        {
            bool check = true;
            try
            {
                cs.VoucherDetails.Add(new VoucherDetail { idproduct = vd.idproduct, idvoucher = vd.idvoucher, status = vd.status });
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }
        #endregion

        #region Cart
        public bool insertCart(int idproduct, int amount, int idClient)
        {
            bool check = true;
            try
            {
                var checkCart = (from c in cs.Carts
                                 where
                                 c.idclient == idClient && c.idproduct == idproduct && c.status == false
                                 select c).SingleOrDefault();
                if (checkCart == null)
                {
                    check = newCart(idproduct, amount, idClient);
                }
                else
                {
                    updateData ud = new updateData();
                    check = ud.updateCart(checkCart.id, amount, idClient);
                }
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool newCart(int idproduct, int amount, int idclient)
        {
            getData gt = new getData();
            bool check = true;
            try
            {
                cs.Carts.Add(new Cart
                {
                    idproduct = idproduct,
                    idclient = idclient,
                    datecart = DateTime.Now,
                    amount = amount,
                    status = false,
                    price = gt.getPriceProduct(idproduct) * amount,
                });
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
        public bool insertOrder(int idproduct, int amount, int idClient)
        {
            bool check = true;
            try
            {
                var checkOrder = (from o in cs.ClientOrders
                                  where o.idclient == idClient && o.status == "0" && o.idproduct == idproduct
                                  select o).FirstOrDefault();
                if (checkOrder == null)
                {
                    check = newOrder(idproduct, amount, idClient);
                }
                else
                {
                    updateData ud = new updateData();
                    check = ud.updateOrder(checkOrder.id, amount, idClient);
                }
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        public bool newOrder(int idproduct, int amount, int idClient)
        {
            bool check = true;
            getData gt = new getData();
            try
            {
                cs.ClientOrders.Add(new ClientOrder
                {
                    idproduct = idproduct,
                    amount= amount,
                    idclient= idClient,
                    datecart =DateTime.Now,
                    status= "0",
                    price=gt.getPriceProduct(idproduct) * amount,
                });
                cs.SaveChanges();
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }
        #endregion
    }


}