﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ErrorTracker
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ErrorTrackerEntities1 : DbContext
    {
        public ErrorTrackerEntities1()
            : base("name=ErrorTrackerEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
    }
}
