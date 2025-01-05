using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceTest
{
    [TestFixture]
    public class ShouldValidateDatabaseConnectionString()
    {
        private IConfiguration _configuration;
        [SetUp]
        public void SetUp()
        {
            var configurationBuilder = 
                new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); 
            _configuration = configurationBuilder.Build();
        }
        [Test]
        public void ValidateIfConnectionStringExists()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            Assert.IsNotNull(connectionString);
        }
        [Test]
        public void ValidateConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using(var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                Assert.That(connection.State, Is.EqualTo(ConnectionState.Open));
            }
        }
    }
}
