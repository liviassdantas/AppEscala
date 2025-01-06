using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => { 
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50); 
                entity.Property(e => e.BirthdayDate).IsRequired().HasMaxLength(10); 
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(14);
                entity.OwnsOne(e => e.Email, a => { a.Property(addressEmail => addressEmail.EmailAddress).HasColumnName("Email"); });
                entity.Property(e => e.Team).IsRequired(); 
                entity.Property(e => e.Team_Function);
                entity.Property(e => e.IsLeader);
            }); 
                
            base.OnModelCreating(modelBuilder);
        }
    }
}


