﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmarkHealthKidoPack.Models;

namespace SmarkHealthKidoPack.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20200705114651_testing1")]
    partial class testing1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmarkHealthKidoPack.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminName")
                        .IsRequired();

                    b.Property<string>("Passward")
                        .IsRequired();

                    b.HasKey("AdminId");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.Child", b =>
                {
                    b.Property<int>("ChildId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChildName");

                    b.Property<int>("age");

                    b.Property<int>("guardianId");

                    b.Property<int>("height");

                    b.Property<string>("password");

                    b.Property<int>("weight");

                    b.HasKey("ChildId");

                    b.HasIndex("guardianId");

                    b.ToTable("children");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.ChildFood", b =>
                {
                    b.Property<int>("ChildId");

                    b.Property<int>("FoodId");

                    b.HasKey("ChildId", "FoodId");

                    b.HasIndex("FoodId");

                    b.ToTable("childFoods");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FoodName")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<decimal>("Price");

                    b.Property<int>("foodCalories")
                        .HasMaxLength(3);

                    b.Property<int>("foodCategoryId");

                    b.Property<string>("photopath");

                    b.HasKey("FoodId");

                    b.HasIndex("foodCategoryId");

                    b.ToTable("food");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.FoodCategory", b =>
                {
                    b.Property<int>("FoodCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FoodCategoryName")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("MessId");

                    b.HasKey("FoodCategoryId");

                    b.HasIndex("MessId");

                    b.ToTable("foodCategories");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.Guardian", b =>
                {
                    b.Property<int>("GuardianId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GuardianName");

                    b.Property<string>("adress");

                    b.Property<int>("messId");

                    b.Property<string>("passward");

                    b.Property<string>("phonenumber");

                    b.HasKey("GuardianId");

                    b.HasIndex("messId");

                    b.ToTable("guardians");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.Mess", b =>
                {
                    b.Property<int>("MessId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MessName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("photopath");

                    b.HasKey("MessId");

                    b.ToTable("Messes");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.Register", b =>
                {
                    b.Property<int>("RegisterId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("fingerprints");

                    b.HasKey("RegisterId");

                    b.ToTable("Registers");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.Testing", b =>
                {
                    b.Property<int>("TestingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdminId");

                    b.Property<string>("Name");

                    b.Property<string>("adminname");

                    b.HasKey("TestingId");

                    b.HasIndex("AdminId");

                    b.ToTable("messages2");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.childfoodviewmodel", b =>
                {
                    b.Property<int>("childfoodviewmodelid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("childid");

                    b.Property<int>("foodid");

                    b.HasKey("childfoodviewmodelid");

                    b.ToTable("childfoodviewmodels");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.Child", b =>
                {
                    b.HasOne("SmarkHealthKidoPack.Models.Guardian", "Guardian")
                        .WithMany("child")
                        .HasForeignKey("guardianId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.ChildFood", b =>
                {
                    b.HasOne("SmarkHealthKidoPack.Models.Child", "child")
                        .WithMany("ChildFoods")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SmarkHealthKidoPack.Models.Food", "Food")
                        .WithMany("ChildFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.Food", b =>
                {
                    b.HasOne("SmarkHealthKidoPack.Models.FoodCategory", "foodCategory")
                        .WithMany("food")
                        .HasForeignKey("foodCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.FoodCategory", b =>
                {
                    b.HasOne("SmarkHealthKidoPack.Models.Mess", "Mess")
                        .WithMany()
                        .HasForeignKey("MessId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.Guardian", b =>
                {
                    b.HasOne("SmarkHealthKidoPack.Models.Mess", "Mess")
                        .WithMany("guardians")
                        .HasForeignKey("messId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.Testing", b =>
                {
                    b.HasOne("SmarkHealthKidoPack.Models.Admin", "admin")
                        .WithMany()
                        .HasForeignKey("AdminId");
                });
#pragma warning restore 612, 618
        }
    }
}
