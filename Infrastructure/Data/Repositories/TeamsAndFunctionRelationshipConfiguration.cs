using Core.Entities;
using Core.Enums;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class TeamsAndFunctionsRelationshipConfiguration
{
    public void Configure(OwnedNavigationBuilder<User, TeamsAndFunctions> teams)
    {
        teams.WithOwner().HasForeignKey("UserId");
        teams.ToTable("TeamsAndFunctions");

        teams.Property<int>("Id"); 
        teams.HasKey("Id");

        teams.Property(t => t.Teams)
             .HasColumnName("Teams")
             .HasConversion(new EnumToStringConverter<Team>())
             .IsRequired();

        teams.Property(f => f.Functions)
             .HasColumnName("Functions")
             .HasConversion(new EnumToStringConverter<Team_Function>())
             .IsRequired();
    }
}
