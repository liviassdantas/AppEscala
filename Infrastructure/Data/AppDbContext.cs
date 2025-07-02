using Core.Entities;
using Core.Enums;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
            ConfigureEmailAddress(entity);
            ConfigureTeams(entity);
            ConfigurePhoneNumber(entity);
        }
        private void ConfigureEmailAddress(EntityTypeBuilder<User> entity)
        {
            entity.OwnsOne(e => e.Email, a => { a.Property(addressEmail => addressEmail.EmailAddress).HasColumnName("Email").IsRequired(); });
        }
        private void ConfigurePhoneNumber(EntityTypeBuilder<User> entity)
        {
            entity.OwnsOne(e => e.PhoneNumber, a => { a.Property(number => number.Number).HasColumnName("PhoneNumber").IsRequired(); });
        }
        private void ConfigureTeams(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(u => u.Id);

            // Outras configurações da entidade User
            entity.OwnsMany(u => u.Teams, new TeamsAndFunctionsRelationshipConfiguration().Configure);
        }

    }
}


