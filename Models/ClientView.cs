using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeShop.Models
{
    public class ClientView
    {
        public int id { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please Input Password")]
        public string password { get; set; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Please Input Confirm Password")]
        [Compare("password", ErrorMessage = "Confirm Password doesn't match,please input again")]
        public string confirmPassword { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please Input Your Email")]
        [RegularExpression("[a-z0-9]+@gmail.com", ErrorMessage = "Please Input Correct Email,example: examplemail@gmail.com")]
        public string email { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Please Input Your Name")]
        public string name { get; set; }

        [DisplayName("Gender")]
        public byte gender { get; set; }

        [DisplayName("Phone")]
        [Required(ErrorMessage = "Please Input Your Phone")]
        [RegularExpression("\\d+", ErrorMessage = "This Field Only Contains Number")]
        public string phone { get; set; }

        [DisplayName("Birth")]
        [Required(ErrorMessage = "Please Input Your BirthDay")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime birth { get; set; }

        [DisplayName("Status")]
        public bool status { get; set; }


    }
}