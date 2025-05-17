using Core.Entities;
using Core.Interfaces;
using Core.ValueObjects;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            if (await UserExistsByEmailAsync(user.Email.EmailAddress))
            {
                throw new InvalidOperationException($"O usuário {user.Name} já está cadastrado no sistema.");
            }
           await _context.Users.AddAsync(user);
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            var user  = await _context.Users.Include(e => e.Email).FirstOrDefaultAsync(u => u.Email.EmailAddress == email);
            return user;
        }

        public async Task<User> FindUserByPhoneNumberAsync(string phoneNumber)
        {
            var user = await _context.Users.Include(e => e.PhoneNumber).FirstOrDefaultAsync(u => u.PhoneNumber.Number == phoneNumber);
            return user;
        }

        public async Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Users.AnyAsync(u => u.PhoneNumber.Number == phoneNumber);
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.EmailAddress == email);
        }
        public async Task FindUserAndDeleteByEmailAsync(string email)
        {
            var user = await FindUserByEmailAsync(email);
            if ( user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
               throw new InvalidOperationException("O usuário não pôde ser localizado.");
            }
        }

        public async Task UpdateAsync(User user)
        {
            var existingUser = await _context.Users.Include(e => e.Email)
                                                   .Include(p => p.PhoneNumber)
                                                   .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existingUser == null)
            {
                throw new InvalidOperationException("O usuário não pôde ser localizado.");
            }

            if (await UserExistsByEmailAsync(user.Email.EmailAddress) && existingUser.Email.EmailAddress != user.Email.EmailAddress)
            {
                throw new InvalidOperationException($"Esse email {user.Email.EmailAddress} já está sendo utilizado.");
            }

            if (await UserExistsByPhoneNumberAsync(user.PhoneNumber.Number) && existingUser.PhoneNumber.Number != user.PhoneNumber.Number)
            {
                throw new InvalidOperationException($"Esse número de telefone {user.PhoneNumber.Number} já está sendo utilizado.");
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.BirthdayDate = user.BirthdayDate;
            existingUser.Team = user.Team;
            existingUser.Team_Function = user.Team_Function;
            existingUser.IsLeader = user.IsLeader;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }


        private AppDbContext AppDbContext => _context as AppDbContext;
    }
}
