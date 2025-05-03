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
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comping> Compings { get; set; }
        public DbSet<Donation> Donations { get; set; }

        public DbSet<BloodCategory> BloodCategories { get; set; }

        public DbSet<BloodComping> BloodCompings { get; set; }
        public DbSet<BloodDonation> BloodDonations { get; set; }


        public test2Context (DbContextOptions<test2Context> options)
            : base(options)
        {
        }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          



          
        }
        public DbSet<test2.Models.BloodCategory> BloodCategory { get; set; } = default!;
     



    }

}
