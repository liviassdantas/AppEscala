using Core.Enums;
using Core.Entities;
using Core.ValueObjects;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Data.Repositories;
using Core.Interfaces;
using Infrastructure.Data;

namespace UserServiceTest
{
    public class ShouldCreateAnUserTest
    {

        [Test]
        public void ShouldCreateAnUser()
        {
        //    private AppDbContext context = new AppDbContext();
            User user =
                new User("Fulano",
                "04/03/1997",
                "219999999",
                new Email{ EmailAddress = "fulano@gmail.com"},
                Team.Music_Team,
                Team_Function.Music_Team_Guitar,
                false);

            //var userRepository = new UserRepository().SaveUser(user);

            //Assert.IsTrue(userRepository.Exists(user));
        }
    }
}