using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Security;


namespace CoffeShop.Models.Database
{
    public class getData
    {
        CoffeShopEntities cs;

        public getData()
        {
            cs = new CoffeShopEntities();
        }
        #region Admin
        public List<BranchView> getBranch()
        {
            List<BranchView> ls = new List<BranchView>();
            ls = cs.Branches.Select(d => new BranchView { ID = d.id, Name = d.name }).ToList();
            return ls;
        }

        public List<EmpView> getEmp()
        {
            List<EmpView> ls = new List<EmpView>();
            ls = (from e in cs.Emps
                  join b in cs.Branches on e.branch equals b.id
                  select new EmpView
                  {
                      id = e.id,
                      name = e.name,
                      genderBool = (bool)e.gender,
                      phone = e.phone,
                      pos = e.pos,
                      dwork = (DateTime)e.dwork,
                      branchName = b.name,
                      status = (bool)e.status,
                  }).ToList();
            return ls;
        }

        public List<ClientView> getClient()
        {
            List<ClientView> ls = new List<ClientView>();
            ls = (from c in cs.Clients
                  select new ClientView
                  {
                      id = c.id,
                      name = c.name,
                      phone = c.phone,
                      email = c.email,
                      birth = (DateTime)c.birth,
                      status = (bool)c.status
                  }).ToList();
            return ls;
        }
        #endregion

        #region Marketing
        public List<CategoryView> getCategory()
        {
            List<CategoryView> ls = new List<CategoryView>();
            ls = (from c in cs.Categories
                  select new CategoryView
                  {
                      id = c.id,
                      name = c.name,
                      status = (bool)c.status
                  }).ToList();
            return ls;
        }

        public List<VoucherView> getVoucher()
        {
            List<VoucherView> ls = new List<VoucherView>();
            ls = cs.Vouchers.Select(v => new VoucherView()
            {
                id = v.id,
                name = v.name,
                status = (bool)v.status,
                datecreate = (DateTime)v.datecreate,
                discount = (int)v.discount
            }).ToList();
            return ls;
        }

        public List<ProductView> getProduct()
        {
            List<ProductView> ls = new List<ProductView>();
            ls = (from p in cs.Products
                  join c in cs.Categories on p.category equals c.id
                  orderby p.id descending
                  select new ProductView
                  {
                      id = p.id,
                      name = p.name,
                      imagename = p.image,
                      description = p.description,
                      price = (int)p.price,
                      catename = c.name,
                      status = (bool)p.status
                  }).ToList();
            return ls;
        }

        public CategoryView getCatebyID(int idcate)
        {
            CategoryView cv = new CategoryView();
            var cate = (from c in cs.Categories where c.id == idcate select c).FirstOrDefault();
            cv.status = (bool)cate.status;
            cv.name = cate.name;
            cv.id = cate.id;
            return cv;
        }

        public VoucherView getVoucherbyID(int idvoucher)
        {
            VoucherView cv = new VoucherView();
            var voucher = (from v in cs.Vouchers where v.id == idvoucher select v).FirstOrDefault();
            cv.id = voucher.id;
            cv.name = voucher.name;
            cv.discount = (int)voucher.discount;
            return cv;
        }

        public ProductView getProductbyID(int idproduct)
        {
            ProductView pv = new ProductView();
            var product = (from p in cs.Products
                           join c in cs.Categories on p.category equals c.id
                           where p.id == idproduct
                           select new ProductView
                           {
                               id = p.id,
                               name = p.name,
                               price = (int)p.price,
                               imagename = p.image,
                               description = p.description,
                               status = (bool)p.status,
                               catename = c.name,
                               category = c.id
                           }).FirstOrDefault();
            pv = product;
            return pv;
        }

        public List<ProductView> vdProduct()
        {
            List<ProductView> ls = new List<ProductView>();
            ls = ((from p in cs.Products
                   where !cs.VoucherDetails.Any(vd => vd.idproduct == p.id
                  && vd.status == true)
                   select new ProductView
                   {
                       id = p.id,
                       name = p.name,
                   })).ToList();
            return ls;
        }

        public List<VoucherView> vdVoucher()
        {
            List<VoucherView> ls = new List<VoucherView>();
            ls = (from v in cs.Vouchers
                  where v.status == true
                  select new VoucherView
                  {
                      id = v.id,
                      name = v.name,
                  }).ToList();
            return ls;
        }

        public List<VoucherDetailView> getVoucherDetails()
        {
            List<VoucherDetailView> ls = new List<VoucherDetailView>();
            ls = (from vd in cs.VoucherDetails
                  join p in cs.Products on vd.idproduct equals p.id
                  join v in cs.Vouchers on vd.idvoucher equals v.id
                  select new VoucherDetailView
                  {
                      id = vd.id,
                      discount = (int)v.discount,
                      price = (int)p.price,
                      status = (bool)vd.status,
                      vouchername = v.name,
                      productname = p.name
                  }).ToList();
            return ls;
        }
        #endregion

        public List<ProductView> getMenu()
        {
            List<ProductView> ls = new List<ProductView>();
            ls = (from p in cs.Products
                  join c in cs.Categories on p.category equals c.id
                  where c.name == "Menu" && p.status == true
                  orderby p.id descending
                  select new ProductView
                  {
                      name = p.name,
                      price = (int)p.price,
                      imagename = p.image,
                      description = p.description,
                  }).ToList();
            return ls;
        }
        #region ProductPage
        public List<ProductView> getProductDefault()
        {
            List<ProductView> ls = new List<ProductView>();
            ls = (from p in cs.Products
                  join c in cs.Categories on p.category equals c.id
                  where !cs.VoucherDetails.Any(vd => vd.idproduct == p.id && vd.status == true)
                  && p.status == true && c.name != "Menu"
                  select new ProductView
                  {
                      id = p.id,
                      name = p.name,
                      price = (int)p.price,
                      imagename = p.image
                  }).ToList();
            return ls;
        }

        public List<VoucherDetailView> getProductSale()
        {
            List<VoucherDetailView> ls = new List<VoucherDetailView>();
            ls = (from vd in cs.VoucherDetails
                  join p in cs.Products on vd.idproduct equals p.id
                  join v in cs.Vouchers on vd.idvoucher equals v.id
                  where vd.status == true && p.status == true
                  select new VoucherDetailView
                  {
                      id = p.id,
                      productname = p.name,
                      price = (int)p.price,
                      discount = (int)v.discount,
                      imagename = p.image,
                      idproduct = p.id,
                  }).ToList();
            return ls;
        }

        public ProductView productDetailID(int idproduct)
        {
            ProductView pv = new ProductView();
            var voucherp = (from vd in cs.VoucherDetails
                            join p in cs.Products on vd.idproduct equals p.id
                            join c in cs.Categories on p.category equals c.id
                            join v in cs.Vouchers on vd.idvoucher equals v.id
                            where vd.id == idproduct && vd.status == true
                            select new ProductView
                            {
                                id = p.id,
                                name = p.name,
                                price = (int)p.price,
                                description = p.description,
                                discount = (int)v.discount,
                                imagename = p.image,
                                catename = c.name
                            }).FirstOrDefault();
            var product = (from p in cs.Products
                           join c in cs.Categories on p.category equals c.id
                           where p.id == idproduct
                           select new ProductView
                           {
                               id = p.id,
                               name = p.name,
                               price = (int)p.price,
                               description = p.description,
                               imagename = p.image,
                               catename = c.name
                           }).FirstOrDefault();
            if (voucherp != null)
            {
                pv = voucherp;
            }
            else
            {
                pv = product;
            }
            return pv;
        }
        #endregion

        #region OrderPage
        public List<ProductView> getOrderDefault()
        {
            List<ProductView> ls = new List<ProductView>();
            ls = (from p in cs.Products
                  join c in cs.Categories on p.category equals c.id
                  where !cs.VoucherDetails.Any(vd => vd.idproduct == p.id && vd.status == true)
                  && p.status == true && c.name == "Menu"
                  select new ProductView
                  {
                      id = p.id,
                      name = p.name,
                      price = (int)p.price,
                      imagename = p.image
                  }).ToList();
            return ls;
        }

        public List<VoucherDetailView> getOrderSale()
        {
            List<VoucherDetailView> ls = new List<VoucherDetailView>();
            ls = (from vd in cs.VoucherDetails
                  join p in cs.Products on vd.idproduct equals p.id
                  join v in cs.Vouchers on vd.idvoucher equals v.id
                  join c in cs.Categories on p.category equals c.id
                  where vd.status == true && p.status == true && c.name == "Menu"
                  select new VoucherDetailView
                  {
                      id = p.id,
                      productname = p.name,
                      price = (int)p.price,
                      discount = (int)v.discount,
                      imagename = p.image

                  }).ToList();
            return ls;
        }

        public ProductView orderDetailID(int idproduct)
        {
            ProductView pv = new ProductView();
            var voucherp = (from vd in cs.VoucherDetails
                            join p in cs.Products on vd.idproduct equals p.id
                            join c in cs.Categories on p.category equals c.id
                            join v in cs.Vouchers on vd.idvoucher equals v.id
                            where vd.id == idproduct && vd.status == true
                            select new ProductView
                            {
                                id = p.id,
                                name = p.name,
                                price = (int)p.price,
                                description = p.description,
                                discount = (int)v.discount,
                                imagename = p.image,
                                catename = c.name
                            }).FirstOrDefault();
            var product = (from p in cs.Products
                           join c in cs.Categories on p.category equals c.id
                           where p.id == idproduct
                           select new ProductView
                           {
                               id = p.id,
                               name = p.name,
                               price = (int)p.price,
                               description = p.description,
                               imagename = p.image,
                               catename = c.name
                           }).FirstOrDefault();
            if (voucherp != null)
            {
                pv = voucherp;
            }
            else
            {
                pv = product;
            }
            return pv;
        }
        #endregion

        public int getPriceProduct(int idproduct)
        {
            int Price = 0;
            try
            {
                var productSale = (from vd in cs.VoucherDetails
                                   join p in cs.Products
                               on vd.idproduct equals p.id
                                   join v in cs.Vouchers on vd.idvoucher equals v.id
                                   where p.id == idproduct
                                   select new VoucherDetailView
                                   {
                                       price = (int)p.price,
                                       discount = (int)v.discount,
                                   }).SingleOrDefault();
                if (productSale != null)
                {
                    Price = productSale.price - (productSale.price * productSale.discount / 100);
                }
                else
                {
                    var product = (from p in cs.Products where p.id == idproduct select p).SingleOrDefault();
                    Price = (int)product.price;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Price;
        }

        #region Login
        public string checkUserAccount(string email, string password)
        {
            string Roles = "";
            try
            {
                var admin = cs.Admins.Any(a => a.email == email && a.password == password);
                var client = cs.Clients.Any(c => c.email == email && c.password == password && c.status == true);
                var emp = cs.Emps.Any(e => e.email == email && e.password == password && e.status == true);
                if (admin != false)
                {
                    Roles = "Admin";
                }
                else if (client != false)
                {
                    Roles = "Client";
                }
                else
                {
                    Roles = "Emp";
                }
            }
            catch (Exception)
            {
                Roles = "Error";
            }
            return Roles;
        }

        public int getIDClient(string email, string password)
        {
            int id = 0;
            try
            {
                var client = cs.Clients.Where(c => c.email == email && c.password == password).SingleOrDefault();
                if (client != null)
                {
                    id = client.id;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return id;
        }

        public EmpView getEmpInfo(string email, string password)
        {
            EmpView ev = new EmpView();
            try
            {
                var emp = cs.Emps.Where(e => e.email == email && e.password == password && e.status == true).SingleOrDefault();
                if (emp != null)
                {
                    ev.id = emp.id;
                    ev.pos = emp.pos;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ev;
        }
        #endregion

        #region Cart
        public List<CartView> getUserCart(int idUser)
        {
            List<CartView> ls = new List<CartView>();
            try
            {
                ls = (from c in cs.Carts
                      join p in cs.Products on c.idproduct equals p.id
                      where c.idclient == idUser && c.status == false
                      select new CartView
                      {
                          id = c.id,
                          productname = p.name,
                          imagename = p.image,
                          amount = c.amount,
                          subtotal = (int)c.price,
                          productprice = (int)p.price
                      }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return ls;
        }
        #endregion

        public List<OrderView> getUserOrder(int idUser)
        {
            List<OrderView> ls = new List<OrderView>();
            try
            {
                ls = (from o in cs.ClientOrders
                      join p in cs.Products on o.idproduct equals p.id
                      where o.idclient == idUser && o.status != "2"
                      select new OrderView
                      {
                          id = o.id,
                          productname = p.name,
                          imagename = p.image,
                          amount = o.amount,
                          subtotal = (int)o.price,
                          productprice = (int)p.price,
                          status = o.status
                      }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return ls;
        }

        public List<OrderView> getClientOrder()
        {
            List<OrderView> ls = new List<OrderView>();
            ls = (from o in cs.ClientOrders
                  join c in cs.Clients on o.idclient equals c.id
                  join p in cs.Products on o.idproduct equals p.id
                  where o.status != "2"
                  orderby c.id
                  select new OrderView
                  {
                      id = o.id,
                      productname = p.name,
                      amount = o.amount,
                      price = o.price,
                      status = o.status,
                      clientName = c.name,
                      clientPhone = c.phone
                  }).ToList();
            return ls;
        }

    }

}