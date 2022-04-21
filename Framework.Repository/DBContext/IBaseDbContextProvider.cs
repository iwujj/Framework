namespace Framework.Repository
{
    public interface IBaseDbContextProvider<TDbContext>  where TDbContext : DbContext
    {
        DbContext GetDbContext();
    }
}
