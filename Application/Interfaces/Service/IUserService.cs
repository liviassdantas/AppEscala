using Application.DTO;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IUserService
    {
        Task<ServiceResult> SaveUser(SaveUserDTO userDTO, UserManager<User> userManager);
        Task<GetUserDTO> GetUserByEmailOrPhone(string? email, string? phoneNumber);
        Task<GetUserDTO> UpdateUser(UpdatedUserDTO userDTO);
    }
}
