using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeShop.Models
{
    public class OrderView
    {
        public int id { get; set; }
        public Nullable<int> idproduct { get; set; }
        public Nullable<int> idclient { get; set; }
        public Nullable<System.DateTime> datecart { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> amount { get; set; }
        public string status { get; set; }

        //Show
        public string productname { get; set; }
        public string imagename { get; set; }
        public int productprice { get; set; }
        public int subtotal { get; set; }
        public string clientName { get; set; }
        public string clientPhone { get; set; }
    }
}