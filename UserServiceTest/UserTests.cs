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
            var configurationBuilder =
                new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = configurationBuilder.Build();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(_configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(10, 5))).Options;
            _context = new AppDbContext(options);
            _userRepository = new UserRepository(_context);
        }
        [Test]
        public async Task ShouldCreateAnUser()
        {
            User user =
                new User("Fulano",
                "04/03/1997",
                "219999999",
                new Email { EmailAddress = "fulanodasilva@gmail.com" },
                Team.Music_Team,
                Team_Function.Music_Team_Guitar,
                false);

            await _userRepository.AddAsync(user);
            await _context.SaveChangesAsync();

            var addedUsers = _userRepository.FindUserByEmailAsync(user.Email.EmailAddress);

            Assert.IsNotNull(addedUsers);
            Assert.That("Fulano", Is.EqualTo(user.Name));
        }
        [Test]
        public void ShouldValidateIfUserExistsBeforeSave()
        {
            User user =
                new User("Fulano",
                "04/03/1997",
                "219999999",
                new Email { EmailAddress = "fulanodasilva@gmail.com" },
                Team.Infantile_Team,
                Team_Function.Infantile_Team_Childreen,
                false);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _userRepository.AddAsync(user));
            Assert.That($"User {user.Name} already exists.", Is.EqualTo(ex.Message));
        }

        [Test]
        public async Task ShouldValidateIfUserExistByEmail()
        {
            string email = "Fulano@gmail.com";

            var userFounded = await _userRepository.FindUserByEmailAsync(email);

            Assert.IsNotNull(userFounded);
            Assert.That("Fulano", Is.EqualTo(userFounded.Name));
        }
    }
}