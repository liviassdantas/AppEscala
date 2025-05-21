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
        public Team Team { get; set; }
        public Team_Function Team_Function { get; set; }
        public bool IsLeader { get; set; }
        public DateTime CreatedAt { get; set; }

        public User() { }

        public User(string name, string birthdayDate, Password password, PhoneNumber phoneNumber, Email emailAddress, Team userTeam, Team_Function userTeamFunction, bool isLeader)
        {
            Name = name;
            BirthdayDate = birthdayDate;
            Password = password;
            PhoneNumber = phoneNumber;
            Email = emailAddress;
            Team = userTeam;
            Team_Function = userTeamFunction;
            IsLeader = isLeader;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
