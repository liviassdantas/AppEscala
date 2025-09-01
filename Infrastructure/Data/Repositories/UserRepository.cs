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
            if (await UserExistsByEmailAsync(user.Email))
            {
                throw new InvalidOperationException($"O usuário {user.Name} já está cadastrado no sistema.");
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            var user = await _context.Users
                             .Include(u => u.Teams)
                              .Include(u => u.Teams)
                                 .ThenInclude(ut => ut.Teams)
                              .FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> FindUserByPhoneNumberAsync(string phoneNumber)
        {
            var user = await _context.Users
                            .Include(u => u.Teams)
                             .Include(u => u.Teams)
                                .ThenInclude(ut => ut.Teams)
                             .FirstOrDefaultAsync(u => u.PhoneNumber.Number == phoneNumber);
            return user;
        }

        public async Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Users.AnyAsync(u => u.PhoneNumber.Number == phoneNumber);
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task FindUserAndDeleteByEmailAsync(string email)
        {
            var user = await FindUserByEmailAsync(email);
            if (user != null)
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

            if (await UserExistsByEmailAsync(user.Email) && existingUser.Email != user.Email)
            {
                throw new InvalidOperationException($"Esse email {user.Email} já está sendo utilizado.");
            }

            if (await UserExistsByPhoneNumberAsync(user.PhoneNumber.Number) && existingUser.PhoneNumber.Number != user.PhoneNumber.Number)
            {
                throw new InvalidOperationException($"Esse número de telefone {user.PhoneNumber.Number} já está sendo utilizado.");
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.BirthdayDate = user.BirthdayDate;
            existingUser.Teams = user.Teams;
            existingUser.IsLeader = user.IsLeader;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }

    }
}
