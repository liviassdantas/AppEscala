using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        Task<int> CompleteAsync();
    }

}
