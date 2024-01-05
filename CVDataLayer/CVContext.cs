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

    // CVContext är en klass som utökar IdentityDbContext och representerar sessionen med databasen.
    // Den innehåller DbSet-egenskaper för varje entitetstyp som ska inkluderas i modellen.

    public class CVContext : IdentityDbContext<Användare>
    {
        // Konstruktor som tar DbContextOptions och skickar dem till bas-konstruktorn.

        public CVContext(DbContextOptions<CVContext> options) : base(options)

        { }

        // DbSet för att hantera användarinformation i databasen.
        public DbSet<Användare> Users { get; set; }

        // DbSet för att hantera personinformation.
        public DbSet<Person> Personer { get; set; }

        // DbSet för att hantera projekten i databasen.
        public DbSet<Projekt> Projekts { get; set; }

        // DbSet för att hantera CV-information.
        public DbSet<CV> CVs { get; set; }

        // DbSet för att hantera flera projekt.

        public DbSet<Projekt> Projekten { get; set; }

        // DbSet för att hantera profiler.

        public DbSet<Profile> Profiler { get; set; }


        // DbSet för att hantera relationer mellan personer och projekt.

        public DbSet<DeltarProjekt> PersonDeltarProjekt { get; set; }


        // DbSet för att hantera meddelanden.

        public DbSet<Meddelande> Meddelande { get; set; }

        // OnModelCreating används för att konfigurera modellen och dess relationer via Fluent API.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigurera sammansatt nyckel för DeltarProjekt.

            modelBuilder.Entity<DeltarProjekt>()
        .HasKey(dp => new { dp.Deltagare, dp.Projekt });

            // Definiera relationen mellan DeltarProjekt och Användare samt Projekt.
            // Specificera att ingen åtgärd sker vid borttagning för att förhindra kaskadborttagning.

            modelBuilder.Entity<DeltarProjekt>()
                .HasOne(dp => dp.Anv)
                .WithMany()
                .HasForeignKey(dp => dp.Deltagare)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DeltarProjekt>()
                .HasOne(dp => dp.Proj)
                .WithMany()
                .HasForeignKey(dp => dp.Projekt)
                .OnDelete(DeleteBehavior.NoAction);

            // Definiera en- till många-relationen mellan Användare och Projekten de skapat.

            modelBuilder.Entity<Användare>()
            .HasMany(u => u.SkapadeProjekt)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.AnvändarId);

           
        }

    }
         
}
