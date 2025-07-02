using Core.Enums;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthdayDate { get; set; }
        public Password Password { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email Email { get; set; }
        public ICollection<TeamsAndFunctions> Teams { get; set; }
        public bool IsLeader { get; set; }
        public DateTime CreatedAt { get; set; }

        public User() { }

        public User(string name, string birthdayDate, Password password, PhoneNumber phoneNumber, Email emailAddress, ICollection<TeamsAndFunctions> teams, bool isLeader)
        {
            Name = name;
            BirthdayDate = birthdayDate;
            Password = password;
            PhoneNumber = phoneNumber;
            Email = emailAddress;
            Teams = teams;
            IsLeader = isLeader;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
