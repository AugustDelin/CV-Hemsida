using System;
using CVModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace CVDataLayer
{
  
    public class CVContext : IdentityDbContext<Användare>
    {

       
        public CVContext(DbContextOptions<CVContext> options) : base(options)

        { }
        public DbSet<Användare> Users { get; set; }

        public DbSet<Person> Personer { get; set; }
        public DbSet<Projekt> Projekts { get; set; }
        public DbSet<CV> CVs { get; set; }

        public DbSet<Projekt> Projekten { get; set; }

        public DbSet<Profile> Profiler { get; set; }

        public DbSet<DeltarProjekt> PersonDeltarProjekt { get; set; }

        public DbSet<Meddelande> Meddelande { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Användare>().HasData(
                new Användare
                {
                   
                    
                }

                );

        

        }

    }



     
}
