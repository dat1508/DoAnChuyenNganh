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

    public partial class CONTACT
    {
        public int IdContact { get; set; }
        [Display(Name = "EMAIL")]
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        public string Email { get; set; }
        public Nullable<int> IdUser { get; set; }
        [Display(Name = "EMAIL")]
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        public string Title { get; set; }
        [Display(Name = "EMAIL")]
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        public string Body { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual USER USER { get; set; }
    }
}
