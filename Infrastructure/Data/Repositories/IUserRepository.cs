using Core.Entities;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    namespace Core.Interfaces
    {
        public interface IUserRepository : IRepository<User>
        {
        }

        public interface IUnitOfWork : IDisposable
        {
            IUserRepository UserRepository { get; }
            Task<int> CompleteAsync();
        }
    }

}
