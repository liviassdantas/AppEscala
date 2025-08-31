using Application.DTO;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.ValueObjects;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Moq;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

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

            _mockUser = new User
            {
                Name = "Fulano2",
                Email = new Email { EmailAddress = "fulanodasilva2@gmail.com" },
                PhoneNumber = new PhoneNumber { Number = "+5524994210951" },
                Password = new Password { UserPassword = "12345678" },
                BirthdayDate = "04/03/1997",
                Teams = new List<UserTeam>
                {
                    new UserTeam
                    {
                        Teams = new TeamsAndFunctions
                        {
                            Teams = Team.Music_Team,
                            Functions = Team_Function.Music_Team_Guitar,
                        }
                    }
                },
                IsLeader = false
            };
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

            Assert.That(addedUser, Is.Not.Null);
            Assert.That(_mockUser.Name, Is.EqualTo(addedUser.Name));
        }

        [Test]
        public async Task ShouldValidateUserExistByEmailAsync()
        {
            _mockUserRepository.Setup(repo => repo.FindUserByEmailAsync(_mockUser.Email.EmailAddress))
                           .ReturnsAsync(_mockUser);

            var foundUser = await _mockUserRepository.Object.FindUserByEmailAsync(_mockUser.Email.EmailAddress);

            Assert.That(foundUser, Is.Not.Null);
            Assert.That(foundUser.Name, Is.EqualTo("Fulano2"));
        }

        [Test]
        public async Task ShouldValidatePhoneNumberExistsAsync()
        {
            _mockUserRepository.Setup(repo => repo.UserExistsByPhoneNumberAsync(_mockUser.PhoneNumber.Number))
                     .ReturnsAsync(true);

            var phoneNumberExists = await _mockUserRepository.Object.UserExistsByPhoneNumberAsync(_mockUser.PhoneNumber.Number);

            Assert.That(phoneNumberExists, Is.True);
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
            Assert.That(userIsDeleted, Is.False);
        }
        [Test]
        public async Task ShouldUpdateUserInformation()
        {
            _mockUserRepository.Setup(repo => repo.FindUserByEmailAsync(_mockUser.Email.EmailAddress))
                           .ReturnsAsync(_mockUser);

            await _mockUserRepository.Object.UpdateAsync(_mockUser);

            var updatedUser = await _mockUserRepository.Object.FindUserByEmailAsync(_mockUser.Email.EmailAddress);

            Assert.That(updatedUser, Is.Not.Null);
            Assert.That(_mockUser.Name, Is.EqualTo(updatedUser.Name));
        }
    }
}
