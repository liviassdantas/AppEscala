using Application.DTO;
using Application.Interfaces.Service;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Entities;
using Core.Mapper;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public async Task<ServiceResult> SaveUser(SaveUserDTO userDTO)
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
                        IsLeader = userDTO.IsLeader,
                        BirthdayDate = userDTO.BirthdayDate,
                        Password = new Password { UserPassword = userDTO.Password },
                        CreatedAt = DateTime.Now,
                        Teams = [] 
                    };
                    var mappedTeamsAndFunctions = TeamsAndFunctionsMapper.ToDomainList(userDTO.Teams);
                    var teamsToSaveInUser = CreateUserTeams(userToSave, mappedTeamsAndFunctions);
                    userToSave.Teams = teamsToSaveInUser;


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

        private IList<UserTeam> CreateUserTeams(User user, IList<ITeamsAndFunctions> teams)
        {
            var userTeams = new List<UserTeam>();

            foreach (var team in teams)
            {
                var teamsAndFunctions = new TeamsAndFunctions
                {
                    Teams = team.Teams,
                    Functions = team.Functions,
                    UserTeams = new List<UserTeam>() 
                };

                var userTeam = new UserTeam
                {
                    User = user,
                    Teams = teamsAndFunctions
                };
                teamsAndFunctions.UserTeams.Add(userTeam);

                userTeams.Add(userTeam);
            }

            return userTeams;
        }

        public async Task<GetUserDTO> GetUserByEmailOrPhone(string? email, string? phoneNumber)
        {
            try
            {
                var userFounded = new User();
                if (!String.IsNullOrEmpty(email))
                    userFounded = await _userRepository.FindUserByEmailAsync(email);
                if(!String.IsNullOrEmpty(phoneNumber))
                    userFounded = await _userRepository.FindUserByPhoneNumberAsync(phoneNumber);

                var userToReturn = new GetUserDTO
                {
                    Email = userFounded.Email.EmailAddress,
                    IsLeader = userFounded.IsLeader,
                    Name = userFounded.Name,
                    PhoneNumber = userFounded.PhoneNumber.Number,
                    Teams = GetTeamsAndFunctionsList(userFounded.Teams),
                    BirthdayDate = userFounded.BirthdayDate,
                };
                return userToReturn;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private IList<TeamsAndFunctions> GetTeamsAndFunctionsList(ICollection<UserTeam> teamsList)
        {
            List<TeamsAndFunctions> teamsListToReturn = new List<TeamsAndFunctions>();
            foreach(var userTeam in teamsList)
            {
                var teamsAndFunctions = new TeamsAndFunctions
                {
                    Teams = userTeam.Teams.Teams,
                    Functions = userTeam.Teams.Functions
                };
                teamsListToReturn.Add(teamsAndFunctions);
            }
            return teamsListToReturn;
        }
        private bool VerifyIfDTOFieldsAreEmpty(SaveUserDTO userDTO)
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
