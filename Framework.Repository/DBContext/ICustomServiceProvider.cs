namespace Framework.Repository
{
    public interface ICustomServiceProvider<TDbContext> where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}
