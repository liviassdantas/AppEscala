using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            await _context.Users.AddAsync(user);
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            var user = await _context.Users.Include(e => e.Email).FirstOrDefaultAsync(user => user.Email.EmailAddress == email);
            return user;
        }
        
        private AppDbContext AppDbContext { get { return _context as AppDbContext; } } 
    }
}
