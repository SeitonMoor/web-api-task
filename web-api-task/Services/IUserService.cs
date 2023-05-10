using web_api_task.Models;

namespace web_api_task.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<int> CreateUserAsync(User request);
        Task UpdateUserAsync(int id, User request);
        Task DeleteUserAsync(int id);
    }
}
