using web_api_task.Database;

namespace web_api_task.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IUserStateRepository _userStateRepository;

        public UnitOfWork(ApplicationDbContext dbContext, IUserRepository userRepository, IUserGroupRepository userGroupRepository, IUserStateRepository userStateRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _userGroupRepository = userGroupRepository;
            _userStateRepository = userStateRepository;
        }

        public IUserRepository UserRepository => _userRepository;

        public IUserGroupRepository UserGroupRepository => _userGroupRepository;

        public IUserStateRepository UserStateRepository => _userStateRepository;

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
