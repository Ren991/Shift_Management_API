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

        public DbSet<Shift> Shift { get; set; } 

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id)
                      .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Shift>()
                .HasOne(s => s.User)
                .WithMany() // No inverse navigation property needed as Client is also a User
                .HasForeignKey(s => s.ClientID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Shift>()
                .Property(s => s.ClientID)
                .IsRequired(false); // Permitir valores nulos


            modelBuilder.Entity<Shift>()
                .HasOne(s => s.User)
                .WithMany() // No inverse navigation property needed as Client is also a User
                .HasForeignKey(s => s.BarberID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Shift>()
                .HasOne(s => s.BarberShop)
                .WithMany(b => b.Shifts)
                .HasForeignKey(s => s.BarberShopID);

            modelBuilder.Entity<Shift>()
                .HasOne(s => s.BarberShop);// One Shift belongs to one BarberShop

            // Relación Many-to-Many entre Shift y ServicesAndHaircuts
            // Configuración de la relación Many-to-Many entre Shift y ServicesAndHaircuts
            modelBuilder.Entity<Shift>()
                .HasMany(s => s.Services)
                .WithMany() // No necesitas definir la navegación inversa en ServicesAndHaircuts
                .UsingEntity<Dictionary<string, object>>(
                    "ShiftService",
                    j => j.HasOne<ServicesAndHaircuts>()
                          .WithMany()
                          .HasForeignKey("ServiceId")
                          .OnDelete(DeleteBehavior.Cascade), // Define el comportamiento de eliminación
                    j => j.HasOne<Shift>()
                          .WithMany()
                          .HasForeignKey("ShiftId")
                          .OnDelete(DeleteBehavior.Cascade), // Define el comportamiento de eliminación
                    j =>
                    {
                        j.HasKey("ShiftId", "ServiceId"); // Llave compuesta
                        j.ToTable("ShiftServices");      // Nombre de la tabla intermedia
                    });


        }


    }
}