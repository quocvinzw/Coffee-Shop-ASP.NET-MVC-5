using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace CoffeShop.Models
{
    public static class ActionImage
    {
        public static string uploadFile(HttpPostedFileBase Img)
        {
            string fileName = DateTime.Now.Ticks + Img.FileName;
            Img.SaveAs(HostingEnvironment.MapPath("~/Content/Uploads/") + fileName);
            return fileName;
        }



    }
}