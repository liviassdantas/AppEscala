using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public class PhoneNumber
    {
        private string _phoneNumber;
        private static readonly string pattern = @"^\+55\d{2}9\d{8}$";
        public string Number
        {
            get { return _phoneNumber; }
            set
            {
                if (ValidatePhoneNumber(value))
                {
                    _phoneNumber = value;
                }
                else
                {
                    throw new ArgumentException("Invalid phone number format.");
                }
            }
        }
        private static bool ValidatePhoneNumber(string number) 
        {
            return Regex.IsMatch(number, pattern); 
        }
    }
}
