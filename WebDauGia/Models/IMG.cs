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
    
    public partial class IMG
    {
        public int Id { get; set; }
        public string LinkImg { get; set; }
        public int IdProduct { get; set; }
    
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
