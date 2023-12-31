﻿// <auto-generated />
using System;
using CVDataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CVDataLayer.Migrations
{
    [DbContext(typeof(CVContext))]
    partial class CVContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CVModels.Användare", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CvId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("Privat")
                        .HasColumnType("bit");

                    b.Property<string>("Profilbild")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjektId")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CvId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ProjektId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CVModels.CV", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AnvändarId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Kompetenser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilbildPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TidigareErfarenhet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Utbildningar")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnvändarId");

                    b.ToTable("CVs");
                });

            modelBuilder.Entity("CVModels.DeltarProjekt", b =>
                {
                    b.Property<string>("Deltagare")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Projekt")
                        .HasColumnType("int");

                    b.Property<string>("AnvändareId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CVId")
                        .HasColumnType("int");

                    b.HasKey("Deltagare", "Projekt");

                    b.HasIndex("AnvändareId");

                    b.HasIndex("CVId");

                    b.HasIndex("Projekt");

                    b.ToTable("PersonDeltarProjekt");
                });

            modelBuilder.Entity("CVModels.Meddelande", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Avsändare")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Innehåll")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Läst")
                        .HasColumnType("bit");

                    b.Property<string>("Mottagare")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Mottagare");

                    b.ToTable("Meddelande");
                });

            modelBuilder.Entity("CVModels.Person", b =>
                {
                    b.Property<string>("Personnummer")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnvändarID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Efternamn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Förnamn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Personnummer");

                    b.HasIndex("AnvändarID")
                        .IsUnique();

                    b.ToTable("Personer");
                });

            modelBuilder.Entity("CVModels.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AntalKlick")
                        .HasColumnType("int");

                    b.Property<string>("Användare")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Beskrivning")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Privat")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Användare")
                        .IsUnique()
                        .HasFilter("[Användare] IS NOT NULL");

                    b.ToTable("Profiler");
                });

            modelBuilder.Entity("CVModels.Projekt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AnvändarId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Beskrivning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnvändarId");

                    b.ToTable("Projekt");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CVModels.Användare", b =>
                {
                    b.HasOne("CVModels.CV", "Cv")
                        .WithMany()
                        .HasForeignKey("CvId");

                    b.HasOne("CVModels.Projekt", null)
                        .WithMany("DeltagandeAnvändare")
                        .HasForeignKey("ProjektId");

                    b.Navigation("Cv");
                });

            modelBuilder.Entity("CVModels.CV", b =>
                {
                    b.HasOne("CVModels.Användare", "User")
                        .WithMany("Cvs")
                        .HasForeignKey("AnvändarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CVModels.DeltarProjekt", b =>
                {
                    b.HasOne("CVModels.Användare", null)
                        .WithMany("DeltarIProjekt")
                        .HasForeignKey("AnvändareId");

                    b.HasOne("CVModels.CV", null)
                        .WithMany("DeltarIProjekt")
                        .HasForeignKey("CVId");

                    b.HasOne("CVModels.Användare", "Anv")
                        .WithMany()
                        .HasForeignKey("Deltagare")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CVModels.Projekt", "Proj")
                        .WithMany()
                        .HasForeignKey("Projekt")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Anv");

                    b.Navigation("Proj");
                });

            modelBuilder.Entity("CVModels.Meddelande", b =>
                {
                    b.HasOne("CVModels.Användare", "Användare")
                        .WithMany("MottagnaMeddelanden")
                        .HasForeignKey("Mottagare")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Användare");
                });

            modelBuilder.Entity("CVModels.Person", b =>
                {
                    b.HasOne("CVModels.Användare", "User")
                        .WithOne("Person")
                        .HasForeignKey("CVModels.Person", "AnvändarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CVModels.Profile", b =>
                {
                    b.HasOne("CVModels.Användare", "Anv")
                        .WithOne("Profil")
                        .HasForeignKey("CVModels.Profile", "Användare");

                    b.Navigation("Anv");
                });

            modelBuilder.Entity("CVModels.Projekt", b =>
                {
                    b.HasOne("CVModels.Användare", "User")
                        .WithMany("SkapadeProjekt")
                        .HasForeignKey("AnvändarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CVModels.Användare", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CVModels.Användare", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CVModels.Användare", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CVModels.Användare", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CVModels.Användare", b =>
                {
                    b.Navigation("Cvs");

                    b.Navigation("DeltarIProjekt");

                    b.Navigation("MottagnaMeddelanden");

                    b.Navigation("Person");

                    b.Navigation("Profil");

                    b.Navigation("SkapadeProjekt");
                });

            modelBuilder.Entity("CVModels.CV", b =>
                {
                    b.Navigation("DeltarIProjekt");
                });

            modelBuilder.Entity("CVModels.Projekt", b =>
                {
                    b.Navigation("DeltagandeAnvändare");
                });
#pragma warning restore 612, 618
        }
    }
}
