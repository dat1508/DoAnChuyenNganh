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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BID> BID { get; set; }
        public virtual DbSet<BLOG> BLOG { get; set; }
        public virtual DbSet<BRAND> BRAND { get; set; }
        public virtual DbSet<CATEGORY> CATEGORY { get; set; }
        public virtual DbSet<CATEGORY_BLOG> CATEGORY_BLOG { get; set; }
        public virtual DbSet<CONTACT> CONTACT { get; set; }
        public virtual DbSet<IMG> IMG { get; set; }
        public virtual DbSet<IMG_BLOG> IMG_BLOG { get; set; }
        public virtual DbSet<PRODUCT> PRODUCT { get; set; }
        public virtual DbSet<RANK> RANK { get; set; }
        public virtual DbSet<USER> USER { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<HISTORY> HISTORY { get; set; }
    }
}
