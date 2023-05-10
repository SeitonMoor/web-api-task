using web_api_task.Models;

namespace web_api_task.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByLoginAsync(string login);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<int> CountAsync();
        Task<IEnumerable<User>> GetPageAsync(int pageNumber, int pageSize);
    }
}
