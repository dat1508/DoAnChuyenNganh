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
    
    public partial class CONTACT
    {
        public int IdContact { get; set; }
        public string Email { get; set; }
        public Nullable<int> IdUser { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    
        public virtual USER USER { get; set; }
    }
}
