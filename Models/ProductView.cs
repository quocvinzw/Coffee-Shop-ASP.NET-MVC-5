using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoffeShop.Models
{
    public class ProductView
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Please Input This Field")]
        public string name { get; set; }

        [Required(ErrorMessage ="Please Input This Field")]
        [RegularExpression("\\d+",ErrorMessage ="This Field Only Constraint Number")]
        [Range(1,100000000,ErrorMessage ="Please Input Correect Value")]
        public int price { get; set; }

        public HttpPostedFileBase image { get; set; }

        [Required(ErrorMessage ="Please Input This Field")]
        public string description { get; set; }

        [Required(ErrorMessage = "Please Input This Field")]
        public int category { get; set; }

        public bool status { get; set; }

        //Show
        public string catename { get; set; }
        public string imagename { get; set; }
        public int discount { get; set; }
    }
}