using web_api_task.Models;

namespace web_api_task.Repositories
{
    public interface IUserStateRepository
    {
        Task<UserState> GetByIdAsync(int id);
        Task<IEnumerable<UserState>> GetAllAsync();
        Task AddAsync(UserState userState);
        Task RemoveAsync(UserState userState);
        Task<UserState> GetByCodeAsync(string code);
    }
}
