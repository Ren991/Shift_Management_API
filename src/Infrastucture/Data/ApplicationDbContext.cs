using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }

        public DbSet<ServicesAndHaircuts> ServicesAndHaircuts { get; set; }

        public DbSet<BarberShop> BarberShop { get; set; }

        public DbSet<Day> Day { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         


        }


    }
}