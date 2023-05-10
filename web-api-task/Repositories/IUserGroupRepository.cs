using web_api_task.Models;

namespace web_api_task.Repositories
{
    public interface IUserGroupRepository
    {
        Task<IEnumerable<UserGroup>> GetAllAsync();
        Task<UserGroup> GetByIdAsync(int id);
        Task<UserGroup> GetByCodeAsync(string code);
        Task CreateUserGroupAsync(UserGroup userGroup);
        Task UpdateUserGroupAsync(UserGroup userGroup);
        Task DeleteUserGroupAsync(int id);
    }
}
