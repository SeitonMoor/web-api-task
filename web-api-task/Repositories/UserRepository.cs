using Microsoft.EntityFrameworkCore;
using web_api_task.Database;
using web_api_task.Models;

namespace web_api_task.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.Include(u => u.UserGroup).Include(u => u.UserState).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.Include(u => u.UserGroup).Include(u => u.UserState).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _dbContext.Users.Include(u => u.UserGroup).Include(u => u.UserState).FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                user.UserState = await _dbContext.UserStates.FirstAsync(us => us.Code == "Blocked");
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Users.CountAsync();
        }

        public async Task<IEnumerable<User>> GetPageAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.Users.Include(u => u.UserGroup).Include(u => u.UserState)
                .OrderByDescending(u => u.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
