﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmarkHealthKidoPack.Models;

namespace SmarkHealthKidoPack.Migrations
{
    [DbContext(typeof(MainContext))]
    partial class MainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("SmarkHealthKidoPack.Models.abdullah", b =>
                {
                    b.Property<int>("abdullahId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("abdullahId");

                    b.ToTable("abdullahs");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.aji", b =>
                {
                    b.Property<int>("ajiId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ajiId");

                    b.ToTable("ajis");
                });

            modelBuilder.Entity("SmarkHealthKidoPack.Models.ajiabdullah", b =>
                {
                    b.Property<int>("abdullahId");

                    b.Property<int>("ajiId");

                    b.HasKey("abdullahId", "ajiId");

                    b.HasIndex("ajiId");

                    b.ToTable("ajiabdullahs");
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

            modelBuilder.Entity("SmarkHealthKidoPack.Models.ajiabdullah", b =>
                {
                    b.HasOne("SmarkHealthKidoPack.Models.abdullah", "abdullah")
                        .WithMany("ajiabdullahs")
                        .HasForeignKey("abdullahId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmarkHealthKidoPack.Models.aji", "aji")
                        .WithMany("ajiabdullahs")
                        .HasForeignKey("ajiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
