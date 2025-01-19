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

namespace UserServiceTest
{
    [TestFixture]
    public class UserTests
    {
        private IConfiguration _configuration;
        private AppDbContext _context;
        private UserRepository _userRepository;

        [SetUp]
        public void SetUp()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = configurationBuilder.Build();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(_configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(10, 5))).Options;
            _context = new AppDbContext(options);
            _userRepository = new UserRepository(_context);
        }

        [Test]
        public async Task ShouldCreateUserAsync()
        {
            var user = new User(
                "Fulano",
                "04/03/1997",
                new PhoneNumber { Number = "+5521999999999" },
                new Email { EmailAddress = "fulanodasilva2@gmail.com" },
                Team.Music_Team,
                Team_Function.Music_Team_Guitar,
                false);

            await _userRepository.AddAsync(user);
            await _context.SaveChangesAsync();

            var addedUser = await _userRepository.FindUserByEmailAsync(user.Email.EmailAddress);

            Assert.IsNotNull(addedUser);
            Assert.That(user.Name, Is.EqualTo(addedUser.Name));
        }

        [Test]
        public void ShouldValidateUserExistsBeforeSave()
        {
            var user = new User(
                "Fulano",
                "04/03/1997",
                new PhoneNumber { Number = "+5521999999999" },
                new Email { EmailAddress = "fulanodasilva2@gmail.com" },
                Team.Infantile_Team,
                Team_Function.Infantile_Team_Childreen,
                false);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _userRepository.AddAsync(user));
            Assert.That(ex.Message, Is.EqualTo($"User {user.Name} already exists."));
        }

        [Test]
        public async Task ShouldValidateUserExistByEmailAsync()
        {
            var email = "Fulano@gmail.com";

            var foundUser = await _userRepository.FindUserByEmailAsync(email);

            Assert.IsNotNull(foundUser);
            Assert.That("Fulano", Is.EqualTo(foundUser.Name));
        }

        [Test]
        public async Task ShouldValidatePhoneNumberExistsAsync()
        {
            var phoneNumber = "+5521999999999";

            var phoneNumberExists = await _userRepository.UserExistsByPhoneNumberAsync(phoneNumber);

            Assert.IsTrue(phoneNumberExists);
        }
        [Test]
        public async Task ShouldFindUserAndDeleteByEmailAsync()
        {
            var email = "Fulano@gmail.com";
            await _userRepository.FindUserAndDeleteByEmailAsync(email);
            var userIsDeleted = await _userRepository.UserExistsByEmailAsync(email);
            Assert.IsFalse(userIsDeleted);
        }
        [Test]
        public async Task ShouldUpdateUserInformation()
        {
            var user = new User(
              "Fulano",
              "03/02/1996",
              new PhoneNumber { Number = "+5521999999998" },
              new Email { EmailAddress = "Fulano@gmail.com" },
              Team.Infantile_Team,
              Team_Function.Infantile_Team_Childreen,
              false);

            await _userRepository.UpdateUserInformation(user);


        }
    }
}
