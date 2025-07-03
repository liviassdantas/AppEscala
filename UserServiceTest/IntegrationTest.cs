using Application.DTO;
using Application.Interfaces.Service;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
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
            _mockUser = new UserDTO
            {
                Name = "Fulano2",
                Email = "fulanodasilva2@gmail.com",
                PhoneNumber = "+5524994210951",
                Password = "12345678",
                BirthdayDate = "04/03/1997",
                Teams = new List<TeamsAndFunctions> 
                {
                    new TeamsAndFunctions 
                    { 
                        Teams = Team.Music_Team,
                        Functions = Team_Function.Music_Team_Guitar,
                    }
                },
                IsLeader = false
            };
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

            Assert.That(result.Name, Is.EqualTo(expectedUser.Name));
        }
        [Test]
        public async Task GetUserByEmailOrPhone_ShouldReturnNull_WhenUserNotFound()
        {
            _mockUserService.Setup(s => s.GetUserByEmailOrPhone("fulanodasilva2@gmail.com", null)).ReturnsAsync((UserDTO)null);
            var result = await _userController.GetUserByEmailOrPhone("fulanodasilva2@gmail.com", null);
            Assert.IsNull(result);
        }

    }
}
