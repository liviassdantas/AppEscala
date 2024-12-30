using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public partial class Email
    {
        private string email;
        private static readonly Regex emailRegex = EmailRegex();

        public string EmailAddress
        {
            get { return email; }
            set
            {
                if (IsValidEmail(value))
                {
                    email = value;
                }
                else
                {
                    throw new ArgumentException("Invalid email address format.");
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            return emailRegex.IsMatch(email);
        }

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled)]
        private static partial Regex EmailRegex();
    }

}

