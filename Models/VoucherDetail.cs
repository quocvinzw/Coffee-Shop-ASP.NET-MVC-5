//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoffeShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VoucherDetail
    {
        public int id { get; set; }
        public Nullable<int> idproduct { get; set; }
        public Nullable<int> idvoucher { get; set; }
        public Nullable<bool> status { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}