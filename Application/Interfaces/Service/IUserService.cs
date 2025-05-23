﻿using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IUserService
    {
        Task<ServiceResult> SaveUser(UserDTO userDTO);
        Task<UserDTO> GetUserByEmailOrPhone(string? email, string? phoneNumber);
    }
}
