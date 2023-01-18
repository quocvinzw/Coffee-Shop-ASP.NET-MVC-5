using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeShop.Models
{
    public class VoucherView
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Please Input This Field")]
        public string name { get; set; }

        [Required(ErrorMessage = "Please Input This Field")]
        [RegularExpression("\\d+", ErrorMessage ="This Field Only Contains Number")]
        [Range(2,10,ErrorMessage ="Discount Must Be Between 0-5%")]
        public int discount { get; set; }

        [Required(ErrorMessage = "Please Input This Field")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime datecreate { get; set; }

        public bool status { get; set; }
    }
}