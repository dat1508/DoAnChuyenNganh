﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations;

    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            this.BID = new HashSet<BID>();
            this.BLOG = new HashSet<BLOG>();
            this.CONTACT = new HashSet<CONTACT>();
            this.PRODUCT = new HashSet<PRODUCT>();
            this.PRODUCT1 = new HashSet<PRODUCT>();
            this.HISTORY = new HashSet<HISTORY>();
        }
    
        public int IdUser { get; set; }
        public Nullable<int> IdRank { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không khớp, vui lòng nhập lại")]
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<bool> Admin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BID> BID { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BLOG> BLOG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTACT> CONTACT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCT1 { get; set; }
        public virtual RANK RANK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HISTORY> HISTORY { get; set; }
    }
}
