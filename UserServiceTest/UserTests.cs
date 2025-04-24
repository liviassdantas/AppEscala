using Core.Enums;
using Core.Entities;
using Core.ValueObjects;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Data.Repositories;
using Core.Interfaces;
using Infrastructure.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Identity.Client;
using Moq;

namespace UserServiceTest
{

    [TestFixture]
    public class UserTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private User _mockUser;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();

            _mockUser = new User(
                "Fulano2",
                "04/03/1997",
                new PhoneNumber { Number = "+5524994210951" },
                new Email { EmailAddress = "fulanodasilva2@gmail.com" },
                Team.Music_Team,
                Team_Function.Music_Team_Guitar,
                false
            );
        }

        [Test]
        public async Task ShouldCreateUserAsync()
        {
            _mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                           .Returns(Task.CompletedTask);

            _mockUserRepository.Setup(repo => repo.FindUserByEmailAsync(_mockUser.Email.EmailAddress))
                               .ReturnsAsync(_mockUser);

            await _mockUserRepository.Object.AddAsync(_mockUser);
            var addedUser = await _mockUserRepository.Object.FindUserByEmailAsync(_mockUser.Email.EmailAddress);

            Assert.IsNotNull(addedUser);
            Assert.That(_mockUser.Name, Is.EqualTo(addedUser.Name));
        }

        [Test]
        public async Task ShouldValidateUserExistByEmailAsync()
        {
            _mockUserRepository.Setup(repo => repo.FindUserByEmailAsync(_mockUser.Email.EmailAddress))
                           .ReturnsAsync(_mockUser);

            var foundUser = await _mockUserRepository.Object.FindUserByEmailAsync(_mockUser.Email.EmailAddress);

            Assert.IsNotNull(foundUser);
            Assert.That("Fulano2", Is.EqualTo(foundUser.Name));
        }

        [Test]
        public async Task ShouldValidatePhoneNumberExistsAsync()
        {
            _mockUserRepository.Setup(repo => repo.UserExistsByPhoneNumberAsync(_mockUser.PhoneNumber.Number))
                     .ReturnsAsync(true);

            var phoneNumberExists = await _mockUserRepository.Object.UserExistsByPhoneNumberAsync(_mockUser.PhoneNumber.Number);

            Assert.IsTrue(phoneNumberExists);
        }
        [Test]
        public async Task ShouldFindUserAndDeleteByEmailAsync()
        {
            _mockUserRepository.Setup(repo => repo.FindUserByEmailAsync(_mockUser.Email.EmailAddress))
                          .ReturnsAsync(_mockUser);

            _mockUserRepository.Setup(repo => repo.UserExistsByEmailAsync(_mockUser.Email.EmailAddress))
                               .ReturnsAsync(false);

            await _mockUserRepository.Object.FindUserAndDeleteByEmailAsync(_mockUser.Email.EmailAddress);
            var userIsDeleted = await _mockUserRepository.Object.UserExistsByEmailAsync(_mockUser.Email.EmailAddress);
            Assert.IsFalse(userIsDeleted);
        }
        [Test]
        public async Task ShouldUpdateUserInformation()
        {
            _mockUserRepository.Setup(repo => repo.FindUserByEmailAsync(_mockUser.Email.EmailAddress))
                           .ReturnsAsync(_mockUser);

            await _mockUserRepository.Object.UpdateAsync(_mockUser);

            var updatedUser = await _mockUserRepository.Object.FindUserByEmailAsync(_mockUser.Email.EmailAddress);

            Assert.IsNotNull(updatedUser);
            Assert.That(_mockUser.Name, Is.EqualTo(updatedUser.Name));
        }
    }
}
