using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task AddAsync(User user);
        Task<User> FindUserByEmailAsync(string email);
        Task<User> FindUserByPhoneNumberAsync(string phoneNumber);
        Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber);
        Task<bool> UserExistsByEmailAsync(string email);
        Task FindUserAndDeleteByEmailAsync(string email);
        Task UpdateAsync(User user);
    }

}
