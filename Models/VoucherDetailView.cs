using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeShop.Models
{
    public class VoucherDetailView
    {
        public int id { get; set; }
        [Range(1,100000000000,ErrorMessage ="Please Select Product")]
        public int idproduct { get; set; }

        [Range(1, 100000000000, ErrorMessage = "Please Select Voucher")]
        public int idvoucher { get; set; }
        public bool status { get; set; }

        //Show
        public string productname { get; set; }
        public string vouchername { get; set; }
        public int price { get; set; }
        public int discount { get; set; }
        public string imagename { get; set; }
    }
}