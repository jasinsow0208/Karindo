﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kramatdjati.EDM
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Labkesda1Entities : DbContext
    {
        public Labkesda1Entities()
            : base("name=Labkesda1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblGLAccount> tblGLAccounts { get; set; }
        public virtual DbSet<tblGLAccountType> tblGLAccountTypes { get; set; }
    }
}
