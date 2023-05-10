namespace web_api_task.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IUserGroupRepository UserGroupRepository { get; }
        IUserStateRepository UserStateRepository { get; }

        Task<int> CommitAsync();
    }
}
