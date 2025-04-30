using Application.DTO;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;

namespace HolyEscalasWebService.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserDTO userDTO)
        {
            var result = await _userService.SaveUser(userDTO);
            if (!result.Success)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(SaveUser), new { name = userDTO.Name}, userDTO);
        }
    }
}
