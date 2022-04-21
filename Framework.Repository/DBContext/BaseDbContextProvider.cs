namespace Framework.Repository
{
    /// <summary>
    /// 简单实现Abp.EntityFrameworkCore
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class BaseDbContextProvider<TDbContext> : IBaseDbContextProvider<TDbContext> where TDbContext : DbContext
    {
        readonly ICustomServiceProvider<TDbContext> _customServiceProvider;

        public BaseDbContextProvider(ICustomServiceProvider<TDbContext> customServiceProvider)
        {
            _customServiceProvider=customServiceProvider;
        }
        public DbContext GetDbContext()
        {
            return _customServiceProvider.GetDbContext();
        }
    }
}
