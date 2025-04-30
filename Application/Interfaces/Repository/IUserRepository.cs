using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task AddAsync(User user);
        Task<User> FindUserByEmailAsync(string email);
        Task<User> FindUserByPhoneNumberAsync(string phoneNumber);
        Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber);
        Task<bool> UserExistsByEmailAsync(string email);
        Task UpdateAsync(User user);
    }
}
