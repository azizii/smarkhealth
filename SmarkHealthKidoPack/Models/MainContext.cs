using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }
        public DbSet<Food> food { get; set; }
        public DbSet<Child> children { get; set; }
       
        //    public DbSet<Admin> admin { get; set; }
        public DbSet<FoodCategory> foodCategories { get; set; }
        public DbSet<Guardian> guardians { get; set; }
        
       public DbSet<Register> Registers { get; set; }
       // public DbSet<Register1> Registers1 { get; set; }
        public DbSet<Mess> Messes { get; set; }
      //  public DbSet<aji> ajis { get; set; }
       // public DbSet<abdullah> abdullahs { get; set; }
      //  public DbSet<ajiabdullah> ajiabdullahs { get; set; }
        public DbSet<ChildFood> childFoods { get; set; }
        public DbSet<childfoodviewmodel>  childfoodviewmodels { get; set; }
         public DbSet<Messages>   Messages{ get; set; }
        public DbSet<SelectedChildfoods>  selectedChildfoodss { get; set; }
        public DbSet<fingerdata> fingerdatas {get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodCategory>()
              .HasMany(c => c.food)
              .WithOne(e => e.foodCategory);

            modelBuilder.Entity<Mess>()
         .HasMany(c => c.guardians)
         .WithOne(e => e.Mess);

            modelBuilder.Entity<Guardian>()
         .HasMany(c => c.child)
         .WithOne(e => e.Guardian);


            modelBuilder.Entity<Guardian>()
              .HasMany(c => c.Messages)
              .WithOne(e => e.Guardian);

            modelBuilder.Entity<ChildFood>()
               .HasKey(e => new { e.ChildId, e.FoodId});

          

        modelBuilder.Entity<ChildFood>()
            .HasOne(pt => pt.Food)
            .WithMany(t => t.ChildFoods)
            .HasForeignKey(pt => pt.FoodId).OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<ChildFood>()
                      .HasOne(pt => pt.child)
                      .WithMany(p => p.ChildFoods)
                      .HasForeignKey(pt => pt.ChildId).OnDelete(DeleteBehavior.Restrict); ;


           // modelBuilder.Entity<ajiabdullah>().HasKey(sc => new { sc.abdullahId, sc.ajiId });
        }


    }


}

