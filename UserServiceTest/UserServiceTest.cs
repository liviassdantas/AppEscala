using System.ComponentModel.DataAnnotations;

namespace UserServiceTest
{
    public class Tests
    {

        [Test]
        public void ShouldCreateAnUser()
        {
            User user =
                new User("Fulano",
                "04/03/1997)",
                "219999999",
                new Email("fulano@gmail.com"),
                Team_Music,
                Function_Guitar_Player,
                isLeader = false);
            
            

        }
    }
}