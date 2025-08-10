using Core.DTO;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class UpdatedUserDTO
    {
        public UpdatedUserDTO() { }
        public UpdatedUserDTO(
           string name,
           Email email,
           Password password,
           PhoneNumber phoneNumber,
           string birthdayDate,
           List<TeamsAndFunctionsDTO> teams,
           bool isLeader = false)
        {
            Name = name;
            Email = email.EmailAddress;
            PhoneNumber = phoneNumber.Number;
            Password = password.UserPassword;
            BirthdayDate = birthdayDate;
            IList<TeamsAndFunctionsDTO> teamsList = teams;
            IsLeader = isLeader;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string BirthdayDate { get; set; }
        public IList<TeamsAndFunctionsDTO> Teams { get; set; }
        public bool IsLeader { get; set; } = false;
    }
}
