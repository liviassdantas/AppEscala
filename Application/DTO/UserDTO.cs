using Core.Enums;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class UserDTO
    {
        public UserDTO() { }
        public UserDTO(
           string name,
           Email email,
           PhoneNumber phoneNumber,
           string birthdayDate,
           Team team,
           Team_Function team_Function,
           bool isLeader = false)
        {
            Name = name;
            Email = email.EmailAddress;
            PhoneNumber = phoneNumber.Number;
            BirthdayDate = birthdayDate;
            Team = team;
            Team_Function = team_Function;
            IsLeader = isLeader;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public string Password { get; set; }
        public string BirthdayDate { get; set; }
        public Team Team { get; set; }
        public Team_Function Team_Function { get; set; }
        public bool IsLeader { get; set; } = false;
    }
}
