using Application.DTO;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Core.Entities;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResult> SaveUser(UserDTO userDTO)
        {
            try
            {
                var checkedDTO = VerifyIfDTOFieldsAreEmpty(userDTO);
                if (checkedDTO)
                {
                    var userToSave = new User
                    {
                        Name = userDTO.Name,
                        Email = new Email { EmailAddress = userDTO.Email },
                        PhoneNumber = new PhoneNumber { Number = userDTO.PhoneNumber },
                        Team = userDTO.Team,
                        Team_Function = userDTO.Team_Function,
                        IsLeader = userDTO.IsLeader,
                    };

                    await _userRepository.AddAsync(userToSave);

                    return new ServiceResult
                    {
                        Message = "Usuário cadastrado com sucesso!",
                        Success = true,
                        User = userToSave
                    };

                }
                else
                {
                    return new ServiceResult
                    {
                        Message = "Erro ao salvar o usuário. Verifique se todos os campos estão preenchidos corretamente e tente novamente.",
                        Success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Message = ex.Message,
                    Success = false,
                };
            }
        }

        private bool VerifyIfDTOFieldsAreEmpty(UserDTO userDTO)
        {
            var isValid = true;
            if (userDTO == null) { return !isValid; throw new ArgumentNullException(nameof(userDTO)); };
            if (String.IsNullOrEmpty(userDTO.Name)) { return !isValid; throw new ArgumentNullException(nameof(userDTO.Name)); };
            if (String.IsNullOrEmpty(userDTO.PhoneNumber)) { return !isValid; throw new ArgumentNullException(nameof(userDTO.PhoneNumber)); };
            if (String.IsNullOrEmpty(userDTO.Email)) { return !isValid; throw new ArgumentNullException(nameof(userDTO.Email)); };
            return isValid;
        }
    }
}
