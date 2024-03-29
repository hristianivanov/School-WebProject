﻿// <auto-generated />
using System;
using DevHunter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DevHunter.Data.Migrations
{
    [DbContext(typeof(DevHunterDbContext))]
    [Migration("20240318075836_RenameColumnCompany")]
    partial class RenameColumnCompany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DevHunter.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DevHunter.Data.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Activity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeeCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FoundedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sector")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebsiteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("DevHunter.Data.Models.CompanyTechnologies", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TechnologyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CompanyId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("CompanyTechnologies");
                });

            modelBuilder.Entity("DevHunter.Data.Models.Development", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Developments");
                });

            modelBuilder.Entity("DevHunter.Data.Models.JobOffer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobPosition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("MaxSalary")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("MinSalary")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PlaceToWork")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Seniority")
                        .HasColumnType("int");

                    b.Property<string>("WorkingExperience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkingHours")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("JobOffers");
                });

            modelBuilder.Entity("DevHunter.Data.Models.SavedJobOffer", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JobOfferId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "JobOfferId");

                    b.HasIndex("JobOfferId");

                    b.ToTable("SavedJobOffers");
                });

            modelBuilder.Entity("DevHunter.Data.Models.Technology", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Technologies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("27e2ac32-6a17-42bf-bb26-33f32f4a0b33"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709491632/DevHunter/technology/Go.png",
                            Name = "Go"
                        },
                        new
                        {
                            Id = new Guid("ea0130fa-a618-4841-8c6b-243c69132576"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709496382/DevHunter/technology/HTML5.png",
                            Name = "Html5"
                        },
                        new
                        {
                            Id = new Guid("3972955e-f830-4870-8c27-b99cb4f10ec0"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709495915/DevHunter/technology/Kubernetes.png",
                            Name = "Kubernetes"
                        },
                        new
                        {
                            Id = new Guid("3495bb04-a023-4359-84c6-195353ae1c19"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709496039/DevHunter/technology/Apache.png",
                            Name = "Apache"
                        },
                        new
                        {
                            Id = new Guid("de422914-5599-4d54-98dd-04eb7ec2768b"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709496307/DevHunter/technology/Angular.png",
                            Name = "Angular"
                        },
                        new
                        {
                            Id = new Guid("fcbae05a-3265-41d2-93ad-4a631fa793be"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709495970/DevHunter/technology/Javascript.png",
                            Name = "Javascript"
                        },
                        new
                        {
                            Id = new Guid("df0b6e1d-2882-4d7d-bd34-74d73149baa8"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709491632/DevHunter/technology/Go.png",
                            Name = "Go"
                        },
                        new
                        {
                            Id = new Guid("870b99df-39ee-4a51-8299-655318403198"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709496353/DevHunter/technology/Gitlab.jpg",
                            Name = "Gitlab"
                        },
                        new
                        {
                            Id = new Guid("81bdac65-c259-4e47-bee2-3598e0f6e49a"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709495888/DevHunter/technology/CSS.png",
                            Name = "CSS"
                        },
                        new
                        {
                            Id = new Guid("e90f948b-3fb9-4713-a635-9bf56f2309fe"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709495873/DevHunter/technology/Bootstrap.png",
                            Name = "Bootstrap"
                        },
                        new
                        {
                            Id = new Guid("cfd0bb21-335b-4cba-88a5-c697410d8392"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709495988/DevHunter/technology/React.png",
                            Name = "React"
                        },
                        new
                        {
                            Id = new Guid("f5319ca7-f8ed-4872-9cc9-2fe96c7bea75"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709496410/DevHunter/technology/Python.png",
                            Name = "Python"
                        },
                        new
                        {
                            Id = new Guid("a254d33e-b9d5-4d86-8798-003cf9aba949"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709495978/DevHunter/technology/Django.png",
                            Name = "Django"
                        },
                        new
                        {
                            Id = new Guid("9d75fff3-d2d1-4984-8f1e-9ad3ee6f72c5"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709496008/DevHunter/technology/MongoDb.png",
                            Name = "MongoDb"
                        },
                        new
                        {
                            Id = new Guid("51fc7007-edc0-4d15-b7b6-558dd31951ac"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709496318/DevHunter/technology/Ansible.png",
                            Name = "Ansible"
                        },
                        new
                        {
                            Id = new Guid("c29ce445-1834-4d48-bfbd-a19d00b1de20"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709495978/DevHunter/technology/Django.png",
                            Name = "Django"
                        },
                        new
                        {
                            Id = new Guid("77eb7251-180f-4ecc-8ad5-32e38fac6d0f"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709496362/DevHunter/technology/Flink.png",
                            Name = "Flink"
                        },
                        new
                        {
                            Id = new Guid("9e8a122f-0198-4598-8c98-e602683d4865"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709496341/DevHunter/technology/Ecma.png",
                            Name = "Ecma"
                        },
                        new
                        {
                            Id = new Guid("a66c1453-9748-4c9e-9183-b8f5330ccffc"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709495925/DevHunter/technology/Linux.png",
                            Name = "Linux"
                        },
                        new
                        {
                            Id = new Guid("a3783abd-f007-42d1-9e5f-508903d7259e"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709496395/DevHunter/technology/Typescript.png",
                            Name = "Typescript"
                        },
                        new
                        {
                            Id = new Guid("54b5b559-50ba-407e-b7a2-d669fda5d5a1"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709495940/DevHunter/technology/MariaDb.png",
                            Name = "MariaDb"
                        },
                        new
                        {
                            Id = new Guid("6163100c-a08c-420b-8400-629f15d204c7"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709495957/DevHunter/technology/Nodejs.png",
                            Name = "Nodejs"
                        },
                        new
                        {
                            Id = new Guid("7d0caa7a-38ea-476e-b641-7ae41102b841"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709495897/DevHunter/technology/Docker.jpg",
                            Name = "Docker"
                        },
                        new
                        {
                            Id = new Guid("101af421-d482-4551-90e8-4d59a7a67efb"),
                            ImageUrl = "http://res.cloudinary.com/dlffxtrek/image/upload/v1709496838/DevHunter/technology/Vue.png",
                            Name = "Vue"
                        });
                });

            modelBuilder.Entity("DevHunter.Data.Models.TechnologyDevelopments", b =>
                {
                    b.Property<Guid>("TechnologyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DevelopmentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TechnologyId", "DevelopmentId");

                    b.HasIndex("DevelopmentId");

                    b.ToTable("TechnologiesDevelopments");
                });

            modelBuilder.Entity("DevHunter.Data.Models.TechnologyJobOffers", b =>
                {
                    b.Property<Guid>("JobOfferId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TechnologyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("JobOfferId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("TechnologyJobOffers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DevHunter.Data.Models.Company", b =>
                {
                    b.HasOne("DevHunter.Data.Models.ApplicationUser", "Creator")
                        .WithMany("Companies")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("DevHunter.Data.Models.CompanyTechnologies", b =>
                {
                    b.HasOne("DevHunter.Data.Models.Company", "Company")
                        .WithMany("UsedTechnologies")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevHunter.Data.Models.Technology", "Technology")
                        .WithMany("CompanyTechnologies")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Technology");
                });

            modelBuilder.Entity("DevHunter.Data.Models.JobOffer", b =>
                {
                    b.HasOne("DevHunter.Data.Models.Company", "Company")
                        .WithMany("JobOffers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("DevHunter.Data.Models.SavedJobOffer", b =>
                {
                    b.HasOne("DevHunter.Data.Models.JobOffer", "JobOffer")
                        .WithMany("SavedJobOffers")
                        .HasForeignKey("JobOfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevHunter.Data.Models.ApplicationUser", "User")
                        .WithMany("SavedJobOffers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobOffer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DevHunter.Data.Models.TechnologyDevelopments", b =>
                {
                    b.HasOne("DevHunter.Data.Models.Development", "Development")
                        .WithMany("DevelopmentTechnologies")
                        .HasForeignKey("DevelopmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevHunter.Data.Models.Technology", "Technology")
                        .WithMany("TechnologyDevelopments")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Development");

                    b.Navigation("Technology");
                });

            modelBuilder.Entity("DevHunter.Data.Models.TechnologyJobOffers", b =>
                {
                    b.HasOne("DevHunter.Data.Models.JobOffer", "JobOffer")
                        .WithMany("JobOfferTechnologies")
                        .HasForeignKey("JobOfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevHunter.Data.Models.Technology", "Technology")
                        .WithMany("TechnologyJobOffers")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobOffer");

                    b.Navigation("Technology");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("DevHunter.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("DevHunter.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevHunter.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("DevHunter.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DevHunter.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("SavedJobOffers");
                });

            modelBuilder.Entity("DevHunter.Data.Models.Company", b =>
                {
                    b.Navigation("JobOffers");

                    b.Navigation("UsedTechnologies");
                });

            modelBuilder.Entity("DevHunter.Data.Models.Development", b =>
                {
                    b.Navigation("DevelopmentTechnologies");
                });

            modelBuilder.Entity("DevHunter.Data.Models.JobOffer", b =>
                {
                    b.Navigation("JobOfferTechnologies");

                    b.Navigation("SavedJobOffers");
                });

            modelBuilder.Entity("DevHunter.Data.Models.Technology", b =>
                {
                    b.Navigation("CompanyTechnologies");

                    b.Navigation("TechnologyDevelopments");

                    b.Navigation("TechnologyJobOffers");
                });
#pragma warning restore 612, 618
        }
    }
}
