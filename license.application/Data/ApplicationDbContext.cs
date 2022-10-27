using license.application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace license.application.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<License> License { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<License>(entity => {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Key).IsRequired();
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.HasOne(m => m.License)
                .WithOne(l => l.Machine)
                .HasForeignKey<Machine>(m => m.LicenseId);
            });

         

            base.OnModelCreating(modelBuilder);
        }
    }
}
