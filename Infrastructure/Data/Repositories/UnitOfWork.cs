using Core.Entities;
using Infrastructure.Repositories.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context; private UserRepository _userRepository; 
        public UnitOfWork(AppDbContext context) { _context = context; }
        public IUserRepository UserRepository { get { return _userRepository ??= new UserRepository(_context); } }

        IRepository<User> IUnitOfWork.UserRepository => throw new NotImplementedException();

        public async Task<int> CompleteAsync() { return await _context.SaveChangesAsync(); }
        public void Dispose()
        {
        }
    }
}
