using Microsoft.EntityFrameworkCore;
using web_api_task.Database;
using web_api_task.Models;

namespace web_api_task.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserGroupRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserGroup>> GetAllAsync()
        {
            return await _dbContext.UserGroups.ToListAsync();
        }

        public async Task<UserGroup> GetByIdAsync(int id)
        {
            return await _dbContext.UserGroups.FindAsync(id);
        }

        public async Task<UserGroup> GetByCodeAsync(string code)
        {
            return await _dbContext.UserGroups.FirstOrDefaultAsync(ug => ug.Code == code);
        }

        public async Task CreateUserGroupAsync(UserGroup userGroup)
        {
            await _dbContext.UserGroups.AddAsync(userGroup);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserGroupAsync(UserGroup userGroup)
        {
            _dbContext.Entry(userGroup).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserGroupAsync(int id)
        {
            var userGroup = await _dbContext.UserGroups.FindAsync(id);
            if (userGroup != null)
            {
                _dbContext.UserGroups.Remove(userGroup);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
