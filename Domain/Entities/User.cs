using Core.Enums;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? BirthdayDate { get; set; }
        public PhoneNumber? PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<UserTeam>? Teams { get; set; }
        public bool IsLeader { get; set; }
        public DateTime CreatedAt { get; set; }

        public User() { }

        public User(string name, string birthdayDate, PhoneNumber phoneNumber, string emailAddress, ICollection<UserTeam> teams, bool isLeader)
        {
            Name = name;
            BirthdayDate = birthdayDate;
            PhoneNumber = phoneNumber;
            Email = emailAddress;
            Teams = teams;
            IsLeader = isLeader;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
