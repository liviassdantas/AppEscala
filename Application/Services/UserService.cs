using Application.DTO;
using Core.Interfaces;
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
                        Teams = userDTO.Teams,
                        IsLeader = userDTO.IsLeader,
                        BirthdayDate = userDTO.BirthdayDate,
                        Password = new Password { UserPassword = userDTO.Password },
                        CreatedAt = DateTime.Now
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

        public async Task<UserDTO> GetUserByEmailOrPhone(string? email, string? phoneNumber)
        {
            try
            {
                var userFounded = new User();
                if (!String.IsNullOrEmpty(email))
                    userFounded = await _userRepository.FindUserByEmailAsync(email);
                if(!String.IsNullOrEmpty(phoneNumber))
                    userFounded = await _userRepository.FindUserByPhoneNumberAsync(phoneNumber);

                var userToReturn = new UserDTO
                {
                    Email = userFounded.Email.EmailAddress,
                    IsLeader = userFounded.IsLeader,
                    Name = userFounded.Name,
                    PhoneNumber = userFounded.PhoneNumber.Number,
                    Teams = userFounded.Teams.ToList(),
                    BirthdayDate = userFounded.BirthdayDate,
                };
                return userToReturn;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
