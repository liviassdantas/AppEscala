using Domain.Enums;
using Domain.Model;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceTest
{
    public class ShouldValidateAnEmailAddress
    {
        [Test]
        public void IfEmailIsValid()
        {
            var email = new Email();
            email.EmailAddress = "fulano@gmail.com";
            Assert.That(email.EmailAddress, Is.EqualTo("fulano@gmail.com"));
        }
        [Test]
        public void IfEmailIsNotValid()
        {
            var email = new Email();
            var exception = Assert.Throws<ArgumentException>(() => email.EmailAddress = "fulano@gmail,com");
            Assert.That(exception.Message, Is.EqualTo("Invalid email address format."));
        }
        [Test]
        public void IfEmailHasWhiteSpaces()
        {
            var email = new Email();
            var exception = Assert.Throws<ArgumentException>(() => email.EmailAddress = "fulano@gmail. com");
            Assert.That(exception.Message, Is.EqualTo("Invalid email address format."));
        }
    }
}
