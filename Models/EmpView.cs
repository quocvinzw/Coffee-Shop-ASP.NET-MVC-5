using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CoffeShop.Models
{
    public class EmpView
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Please Input This Field")]
        [RegularExpression("[a-z0-9]+@gmail.com", ErrorMessage = "Please Input Correct Email,example: examplemail@gmail.com")]
        public string email { get; set; }

        [Required(ErrorMessage = "Please Input This Field")]
        public string password { get; set; }

        [Required(ErrorMessage = "Please Input This Field")]
        [Compare("password", ErrorMessage = "Confirm Password doesn't match,please input again")]
        public string confirmpass { get; set; }

        [Required(ErrorMessage = "Please Input This Field")]
        public string name { get; set; }

        public byte gender { get; set; }
        public bool genderSelect { get; set; }

        [Required(ErrorMessage = "Please Input This Field")]
        [RegularExpression("\\d+", ErrorMessage = "This Field Only Contains Number")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Please Input This Field")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",ApplyFormatInEditMode = true)]
        public DateTime birth { get; set; }

        [Required(ErrorMessage = "Please Input This Field")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dwork { get; set; }

        public string pos { get; set; }
            
        [Required]
        public int branch { get; set; }

        public bool status { get; set; }

        //View
        public string branchName { get; set; }
        public bool genderBool { get; set; }


    }
}