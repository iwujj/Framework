namespace Framework.Services
{
    public class UserService : IUserService
    {
        IBaseRepository<User> _userRespository { get; set; }
        IUnitOfWork _unitOfWork { get; set; }
        public UserService(IBaseRepository<User> userRepository, IUnitOfWork uintOfWork)
        {
            _userRespository = userRepository;
            _unitOfWork = uintOfWork;
        }

        public async Task AddUser()
        {
            User user = new User();
            user.Id = Guid.NewGuid();
            user.Name = "李四";
            user.Password = "123";
            user.Age = 18;
            user.Account = "admin";
            user.IsEnabled = true;
            user.IsDelete = false;
            user.Code = "test001";
            await _userRespository.Add(user);
            await _unitOfWork.SaveChangesAsync();
        }
        public  async Task RemoveUser(Expression<Func<User, bool>> predicate)
        {
             _userRespository.Delete(predicate);
              //await Task.CompletedTask;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
