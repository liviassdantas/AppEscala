using Application.DTO;
using Application.Interfaces.Service;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;

namespace HolyEscalasWebService.Controllers
{
    [ApiController]
    [Route("api/user/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// It's save an user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> SaveUser(SaveUserDTO userDTO, UserManager<User> userManager)
        {
            var result = await _userService.SaveUser(userDTO, userManager);
            if (!result.Success)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(SaveUser), new { name = userDTO.Name}, userDTO);
        }

        /// <summary>
        /// Get the user by email or phoneNumber
        /// </summary>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<GetUserDTO> GetUserByEmailOrPhone (string? email, string? phoneNumber)
        {
            var result = await _userService.GetUserByEmailOrPhone(email, phoneNumber);
            return result;
        }

        /// <summary>
        /// It's update an user
        /// </summary>
        /// <param name="updatedUser"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<GetUserDTO> UpdateUser(UpdatedUserDTO updatedUser)
        {
            var result = await _userService.UpdateUser(updatedUser);

            return result;
        }
    }
}
