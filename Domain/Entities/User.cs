using Core.Enums;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User(string name, string birthdayDate, string phoneNumber, Email emailAddress, Team userTeam, Team_Function userTeamFunction, bool isLeader = false)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string BirthdayDate { get; set; } = birthdayDate;
        public string PhoneNumber { get; set; } = phoneNumber;
        public Email Email { get; set; } = emailAddress;
        public Team Team { get; set; } = userTeam;
        public Team_Function Team_Function { get; set; } = userTeamFunction;
        public bool IsLeader { get; set; } = isLeader;
    }
}
