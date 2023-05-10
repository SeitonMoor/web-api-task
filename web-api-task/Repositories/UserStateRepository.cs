using Microsoft.EntityFrameworkCore;
using web_api_task.Database;
using web_api_task.Models;

namespace web_api_task.Repositories
{
    public class UserStateRepository : IUserStateRepository
    {
        private readonly ApplicationDbContext _context;

        public UserStateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserState> GetByIdAsync(int id)
        {
            return await _context.UserStates.FindAsync(id);
        }

        public async Task<IEnumerable<UserState>> GetAllAsync()
        {
            return await _context.UserStates.ToListAsync();
        }

        public async Task AddAsync(UserState userState)
        {
            await _context.UserStates.AddAsync(userState);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(UserState userState)
        {
            _context.UserStates.Remove(userState);
            await _context.SaveChangesAsync();
        }

        public async Task<UserState> GetByCodeAsync(string code)
        {
            return await _context.UserStates.FirstOrDefaultAsync(us => us.Code == code);
        }
    }
}
