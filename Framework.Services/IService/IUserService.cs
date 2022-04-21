namespace Framework.Services
{
    public interface IUserService
    {
        Task AddUser();
        Task RemoveUser(Expression<Func<User, bool>> predicate);
    }
}
