using Core.Entities;
using Core.Enums;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class GetUserDTO
    {
        public GetUserDTO() { }
        public GetUserDTO(
           string name,
           Email email,
           PhoneNumber phoneNumber,
           string birthdayDate,
           List<TeamsAndFunctions> teams,
           bool isLeader = false)
        {
            Name = name;
            Email = email.EmailAddress;
            PhoneNumber = phoneNumber.Number;
            BirthdayDate = birthdayDate;
            IList<TeamsAndFunctions> teamsList = teams;
            IsLeader = isLeader;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthdayDate { get; set; }
        public IList<TeamsAndFunctions> Teams { get; set; } = new List<TeamsAndFunctions>();
        public bool IsLeader { get; set; } = false;
    }
}
