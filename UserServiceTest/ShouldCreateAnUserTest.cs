using Domain.Enums;
using Domain.Model;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace UserServiceTest
{
    public class ShouldCreateAnUserTest
    {

        [Test]
        public void ShouldCreateAnUser()
        {
            User user =
                new User("Fulano",
                "04/03/1997",
                "219999999",
                new Email{ EmailAddress = "fulano@gmail.com"},
                Team.Music_Team,
                Team_Function.Music_Team_Guitar,
                false);

            //var userRepository = new UserRepository(IUnitOfWork _uow).SaveUser(user);
            
            //Assert.IsTrue(userRepository.Exists(user));
        }
    }
}