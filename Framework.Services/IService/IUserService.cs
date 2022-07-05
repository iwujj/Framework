namespace Framework.Services
{
    public interface IUserService
    {
        Task AddUser();
        Task RemoveUser(Expression<Func<User, bool>> predicate);

        Task<User> GetUser(Expression<Func<User, bool>> predicate);
    }
}
