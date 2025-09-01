using Application.DTO;
using Application.Interfaces.Service;
using Core.DTO;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using HolyEscalasWebService.Controllers;
using Microsoft.AspNetCore.Identity;
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
        private SaveUserDTO _mockSaveUser;
        private GetUserDTO _mockGetUser;
        private UserManager<User> _mockUserManager;

        [SetUp]
        public void SetUp()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
            _mockSaveUser = new SaveUserDTO
            {
                Name = "Fulano2",
                Email = "fulanodasilva2@gmail.com",
                PhoneNumber = "+5524994210951",
                BirthdayDate = "04/03/1997",
                Teams = new List<TeamsAndFunctionsDTO>
                {
                    new TeamsAndFunctionsDTO
                    {
                        Teams = Team.Music_Team,
                        Functions = Team_Function.Music_Team_Guitar,
                    }
                },
                IsLeader = false
            };
            _mockGetUser = new GetUserDTO
            {
                Name = "Fulano2",
                Email = "fulanodasilva2@gmail.com",
                PhoneNumber = "+5524994210951",
                BirthdayDate = "04/03/1997",
                Teams =
                [
                    new TeamsAndFunctions
                    {
                        Teams = Team.Music_Team,
                        Functions = Team_Function.Music_Team_Guitar,
                    }
                ],
                IsLeader = false
            };
        }
        [Test]
        public async Task SaveUser_ShouldReturnCreated_WhenSuccess()
        {
            _mockUserService.Setup(s => s.SaveUser(It.IsAny<SaveUserDTO>(), It.IsAny<UserManager<User>>()))
                            .ReturnsAsync(new ServiceResult { Success = true });

            var result = await _userController.SaveUser(_mockSaveUser, _mockUserManager);

            var createdResult = result as CreatedAtActionResult;
            Assert.That(createdResult, Is.Not.Null);
            Assert.That(((SaveUserDTO)createdResult.Value).Name, Is.EqualTo("Fulano2"));
        }

        [Test]
        public async Task SaveUser_ShouldReturnBadRequest_WhenFailure()
        {
            _mockUserService.Setup(s => s.SaveUser(It.IsAny<SaveUserDTO>(), It.IsAny<UserManager<User>>()))
                            .ReturnsAsync(new ServiceResult { Success = false });

            var result = await _userController.SaveUser(_mockSaveUser, _mockUserManager);

            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task GetUserByEmailOrPhone_ShouldReturnUserDTO()
        {
            var expectedUser = _mockGetUser;

            _mockUserService.Setup(s => s.GetUserByEmailOrPhone("fulanodasilva2@gmail.com", null))
                            .ReturnsAsync(expectedUser);

            var result = await _userController.GetUserByEmailOrPhone("fulanodasilva2@gmail.com", null);

            Assert.That(result.Name, Is.EqualTo(expectedUser.Name));
        }

    }
}
