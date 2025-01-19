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
                throw new InvalidOperationException($"User {user.Name} already exists.");
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
               throw new InvalidOperationException("User not found");
            }
        }

        private AppDbContext AppDbContext => _context as AppDbContext;
    }
}
