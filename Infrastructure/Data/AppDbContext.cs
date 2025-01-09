using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
            modelBuilder.Entity<User>(ConfigureUserEntity);
            base.OnModelCreating(modelBuilder);
        }
        private void ConfigureUserEntity(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.BirthdayDate).IsRequired().HasMaxLength(10);
            entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(14);
            ConfigureEmailAddress(entity);
            ConfigureEnumProperties(entity);
        }
        private void ConfigureEmailAddress(EntityTypeBuilder<User> entity)
        {
            entity.OwnsOne(e => e.Email, a => { a.Property(addressEmail => addressEmail.EmailAddress).HasColumnName("Email").IsRequired(); });
        }

        private void ConfigureEnumProperties(EntityTypeBuilder<User> entity)
        {
            var teamConverter = new EnumToStringConverter<Team>();
            entity.Property(e => e.Team).HasConversion(teamConverter);
            var teamFunctionConverter = new EnumToStringConverter<Team_Function>();
            entity.Property(e => e.Team_Function).HasConversion(teamFunctionConverter); entity.Property(e => e.IsLeader);
        }
    }
}


