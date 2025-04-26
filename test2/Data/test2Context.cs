using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using test2.Models;

namespace test2.Data
{
    public class test2Context : DbContext
    {
        public test2Context (DbContextOptions<test2Context> options)
            : base(options)
        {
        }

        public DbSet<test2.Models.Naseem> Naseem { get; set; } = default!;
        public DbSet<test2.Models.khattab> khattab { get; set; } = default!;
        public DbSet<test2.Models.Moh> Moh { get; set; } = default!;
        public DbSet<test2.Models.Parson> Parson { get; set; } = default!;
        public DbSet<test2.Models.Category> Category { get; set; } = default!;
        public DbSet<test2.Models.SubCategory> SubCategory { get; set; } = default!;
        public object SubCategories { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // اختياري: لحذف الفئات الفرعية تلقائيًا إذا انحذفت الفئة الرئيسية

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<test2.Models.User> User { get; set; } = default!;



    }
}
