using System;
using CVModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace CVDataLayer
{
  
    public class CVContext : DbContext 
    {

       
        public CVContext(DbContextOptions<CVContext> options) : base(options)

        { }
        public DbSet<User> Users { get; set; }

        public DbSet<Person> Personer { get; set; }
        public DbSet<Projekt> Projekts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserName = "erik.alingsas@gmail.com",
                    Password = "granlunda",
                    
                }

                );

        }

    }



   
}
