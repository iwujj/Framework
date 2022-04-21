namespace Framework.Services
{
    public class UserService : IUserService
    {
          IBaseRepository<User> _userRespository { get; set; }
          IUintOfWork _uintOfWork { get; set; }
        public UserService(IBaseRepository<User> userRepository, IUintOfWork uintOfWork)
        {
            _userRespository = userRepository;
            _uintOfWork = uintOfWork;
        }

        public async Task AddUser()
        {
            User user = new User();
            user.Id = Guid.NewGuid();
            user.Name = "张三";
            user.Password = "123";
            user.Age = 18;
            user.Account = "admin";
            user.IsEnabled = true;
            user.IsDelete = false;
            user.Code = "test001";
            await _userRespository.Add(user);
            await _uintOfWork.SaveChangesAsync();
        }
    }
}
