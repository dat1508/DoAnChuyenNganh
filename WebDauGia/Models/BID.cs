//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDauGia.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BID
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdProduct { get; set; }
        public Nullable<int> BidPrice { get; set; }
        public Nullable<System.DateTime> BidTime { get; set; }
        public string Status { get; set; }
    
        public virtual PRODUCT PRODUCT { get; set; }
        public virtual USER USER { get; set; }
    }
}