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
    public class ShouldCreateAnUserTest
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
                .UseMySql(_configuration.GetConnectionString("DefaultConnection"),new MySqlServerVersion(new Version(10, 5))).Options;
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
                new Email { EmailAddress = "fulano@gmail.com" },
                Team.Music_Team,
                Team_Function.Music_Team_Guitar,
                false);
           
            await _userRepository.AddAsync(user);
            await _context.SaveChangesAsync();

            var addedUsers = _context.Users.FindAsync(user.Id);

            Assert.IsNotNull(addedUsers);
            Assert.That("Fulano", Is.EqualTo(user.Name));
        }
    }
}