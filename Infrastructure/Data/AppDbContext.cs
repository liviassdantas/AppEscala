using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<TeamsAndFunctions> TeamsAndFunctions { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura entidade User
            modelBuilder.Entity<User>(ConfigureUserEntity);

            // Configura entidade TeamsAndFunctions
            modelBuilder.Entity<TeamsAndFunctions>(entity =>
            {
                entity.ToTable("TeamsAndFunctions");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Teams)
                .IsRequired()
                .HasMaxLength(100)
                .HasConversion(new EnumToStringConverter<Team>());
                entity.Property(t => t.Functions)
                .IsRequired()
                .HasMaxLength(100)
                .HasConversion(new EnumToStringConverter<Team_Function>());
            });

            // Configura tabela de junção muitos-para-muitos
            modelBuilder.Entity<UserTeam>(entity =>
            {
                entity.ToTable("UsuarioTeams");
                entity.HasKey(ut => new { ut.UserId, ut.TeamId });

                entity.HasOne(ut => ut.User)
                      .WithMany(u => u.Teams)
                      .HasForeignKey(ut => ut.UserId);

                entity.HasOne(ut => ut.Teams)
                      .WithMany(t => t.UserTeams)
                      .HasForeignKey(ut => ut.TeamId);
            });

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureUserEntity(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");

            entity.HasKey(u => u.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.BirthdayDate).IsRequired().HasMaxLength(10);
            entity.Property(e => e.IsLeader).IsRequired();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            ConfigurePhoneNumber(entity);
        }
        private void ConfigurePhoneNumber(EntityTypeBuilder<User> entity)
        {
            entity.OwnsOne(e => e.PhoneNumber, a =>
            {
                a.Property(p => p.Number)
                 .HasColumnName("PhoneNumber")
                 .IsRequired();
            });
        }
    }
}
