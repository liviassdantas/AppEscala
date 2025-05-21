using Application.DTO;
using Application.Interfaces.Service;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.ValueObjects;
using HolyEscalasWebService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceTest
{
    [TestFixture]
    public class IntegrationTest
    {
        private Mock<IUserService> _mockUserService;
        private UserController _userController;
        private UserDTO _mockUser;

        [SetUp]
        public void SetUp()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
            _mockUser = new UserDTO(
               "Fulano2",
               new Email { EmailAddress = "fulanodasilva2@gmail.com" },
               new PhoneNumber { Number = "+5524994210951" },
               "04/03/1997",
               Team.Music_Team,
               Team_Function.Music_Team_Guitar,
               false
           );
        }
        [Test]
        public async Task SaveUser_ShouldReturnCreated_WhenSuccess()
        {
            _mockUserService.Setup(s => s.SaveUser(It.IsAny<UserDTO>()))
                            .ReturnsAsync(new ServiceResult { Success = true });

            var result = await _userController.SaveUser(_mockUser);

            var createdResult = result as CreatedAtActionResult;
            Assert.NotNull(createdResult);
            Assert.That(((UserDTO)createdResult.Value).Name, Is.EqualTo("Fulano2"));
        }

        [Test]
        public async Task SaveUser_ShouldReturnBadRequest_WhenFailure()
        {
            _mockUserService.Setup(s => s.SaveUser(It.IsAny<UserDTO>()))
                            .ReturnsAsync(new ServiceResult { Success = false });

            var result = await _userController.SaveUser(_mockUser);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task GetUserByEmailOrPhone_ShouldReturnUserDTO()
        {
            var expectedUser = _mockUser;

            _mockUserService.Setup(s => s.GetUserByEmailOrPhone("fulanodasilva2@gmail.com", null))
                            .ReturnsAsync(expectedUser);

            var result = await _userController.GetUserByEmailOrPhone("fulanodasilva2@gmail.com", null);

            Assert.AreEqual(expectedUser.Name, result.Name);
        }
        [Test]
        public async Task GetUserByEmailOrPhone_ShouldReturnNull_WhenUserNotFound()
        {
            _mockUserService.Setup(s => s.GetUserByEmailOrPhone("fulanodasilva2@gmail.com", null))
                            .ReturnsAsync((UserDTO)null);
            var result = await _userController.GetUserByEmailOrPhone("fulanodasilva2@gmail.com", null);
            Assert.IsNull(result);
        }

    }
}
