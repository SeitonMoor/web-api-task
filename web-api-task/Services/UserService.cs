using web_api_task.Models;
using web_api_task.Repositories;

namespace web_api_task.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IUserStateRepository _userStateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUserGroupRepository userGroupRepository,
            IUserStateRepository userStateRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userGroupRepository = userGroupRepository;
            _userStateRepository = userStateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception($"User with id {id} was not found");
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users;
        }

        public async Task<int> CreateUserAsync(User request)
        {
            var existingUser = await _userRepository.GetByLoginAsync(request.Login);

            if (existingUser != null)
            {
                throw new ArgumentException($"A user with login {request.Login} already exists.");
            }

            var userState = await _userStateRepository.GetByCodeAsync(request.UserState.Code);

            if (userState == null)
            {
                throw new ArgumentException($"Invalid user state code: {request.UserStateId}");
            }

            var userGroup = await _userGroupRepository.GetByCodeAsync(request.UserGroup.Code);

            if (userGroup == null)
            {
                throw new ArgumentException($"Invalid user group code: {request.UserGroup.Code}");
            }

            var newUser = new User
            {
                Login = request.Login,
                Password = request.Password,
                CreatedDate = DateTime.UtcNow,
                UserGroupId = userGroup.Id,
                UserGroup = userGroup,
                UserStateId = userState.Id,
                UserState = userState
            };

            await _userRepository.CreateUserAsync(newUser);
            await _unitOfWork.CommitAsync();

            return newUser.Id;
        }

        public async Task UpdateUserAsync(int id, User request)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception($"User with id {id} not found.");
            }

            var userState = await _userStateRepository.GetByCodeAsync(request.UserState.Code);

            if (userState == null)
            {
                throw new ArgumentException($"Invalid user state code: {request.UserStateId}");
            }

            user.UserState = userState;
            user.UserStateId = userState.Id;

            var userGroup = await _userGroupRepository.GetByCodeAsync(request.UserGroup.Code);

            if (userGroup == null)
            {
                throw new ArgumentException($"Invalid user group code: {request.UserGroup.Code}");
            }

            user.UserGroup = userGroup;
            user.UserGroupId = userGroup.Id;

            user.Login = request.Login;
            user.Password = request.Password;

            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException($"User with id {id} not found");
            }

            await _userRepository.DeleteUserAsync(id);
        }
    }
}
