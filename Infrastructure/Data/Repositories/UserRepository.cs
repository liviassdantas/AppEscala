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
            var userExists = await ValidateIfUserNotExists(user.Email.EmailAddress);
            if (!userExists) 
            {
                await _context.Users.AddAsync(user);
            }
            else
            {
                throw new InvalidOperationException($"User {user.Name} already exists.");
            }
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            var user = await _context.Users.Include(e => e.Email).FirstOrDefaultAsync(user => user.Email.EmailAddress == email);
            return user;
        }

        private async Task<bool> ValidateIfUserNotExists(string email)
        {
            var returnValidation = false;
            var user = await FindUserByEmailAsync(email);
            if (user != null)
            {
                returnValidation = true;
            }
            return returnValidation;
        }
        private AppDbContext AppDbContext { get { return _context as AppDbContext; } }
    }
}
