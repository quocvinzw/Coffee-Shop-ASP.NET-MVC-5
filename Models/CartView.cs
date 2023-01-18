using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeShop.Models
{
    public class CartView
    {
        public int id { get; set; }
        public Nullable<int> idproduct { get; set; }
        public Nullable<int> idclient { get; set; }
        public Nullable<System.DateTime> datecart { get; set; }
        public Nullable<int> amount { get; set; }
        public Nullable<bool> status { get; set; } 

        //Show
        public string productname { get; set; }
        public string imagename { get; set; }
        public int productprice { get; set; }
        public int subtotal { get; set; }
    }
}