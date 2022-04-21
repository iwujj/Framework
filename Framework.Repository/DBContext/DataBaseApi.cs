namespace Framework.Repository
{
    public class DataBaseApi:IDataBaseApi
    {
        public DbContext _dbContext { get; }

        public DataBaseApi(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
