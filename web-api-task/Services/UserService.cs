using Microsoft.EntityFrameworkCore;
using web_api_task.Database;
using web_api_task.Models;
using web_api_task.Repositories;

namespace web_api_task.Services
{
    public class UserService : IUserService
    {
        public Task<User> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateUserAsync(User request)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(int id, User request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
