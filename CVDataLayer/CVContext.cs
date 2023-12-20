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
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Personnummer = "1234567890",
                    Förnamn = "John",
                    Efternamn = "Doe",
                    Adress = "123 Main St",
                    AnvändarID = "user1"  // Make sure this user ID corresponds to an existing user
                },
        new Person
        {
            Personnummer = "9876543210",
            Förnamn = "Jane",
            Efternamn = "Smith",
            Adress = "456 Oak St",
            AnvändarID = "user2"  // Make sure this user ID corresponds to an existing user
        }

                );
            modelBuilder.Entity<DeltarProjekt>()
        .HasKey(dp => new { dp.Deltagare, dp.Projekt });

            modelBuilder.Entity<DeltarProjekt>()
                .HasOne(dp => dp.Anv)
                .WithMany()
                .HasForeignKey(dp => dp.Deltagare)
                .OnDelete(DeleteBehavior.NoAction); // Specify ON DELETE NO ACTION

            modelBuilder.Entity<DeltarProjekt>()
                .HasOne(dp => dp.Proj)
                .WithMany()
                .HasForeignKey(dp => dp.Projekt)
                .OnDelete(DeleteBehavior.NoAction);



        }

    }



     
}
