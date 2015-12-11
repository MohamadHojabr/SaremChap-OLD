
using Domainclasses.Modes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Domainclasses.Context
{

    public class ApplicationUser : IdentityUser
    {
    }

    public class SaremContext : IdentityDbContext<ApplicationUser>
    {
        public SaremContext()
            : base("SaremContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Value>()
    .HasRequired(d => d.Form)
    .WithMany()
    .HasForeignKey(d => d.FormId)
    .WillCascadeOnDelete(false);
            modelBuilder.Entity<Form>()
    .HasRequired(u => u.Product)
    .WithMany()
    .HasForeignKey(u => u.Product_ID);



        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<ProductFiles> Files { get; set; }
        public DbSet<ProductCategoryFiles> ProductCategoryFiles { get; set; }
        public DbSet<SubjectFiles> SubjectFileses { get; set; }


    }
}
