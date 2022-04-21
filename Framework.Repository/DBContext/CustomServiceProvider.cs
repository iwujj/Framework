using Microsoft.Extensions.DependencyInjection;

namespace Framework.Repository
{
    public class CustomServiceProvider<TDbContext> : ICustomServiceProvider<TDbContext> where TDbContext : DbContext
    {
        private readonly Dictionary<string, IDataBaseApi> _databaseApi;
        private readonly IServiceProvider _serviceProvider;
        public CustomServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _databaseApi = new Dictionary<string, IDataBaseApi>();
        }

        public TDbContext GetDbContext()
        {
            var dbcontextName=typeof(TDbContext).FullName;
            IDataBaseApi dataBaseApi = null;
            _databaseApi.TryGetValue(dbcontextName, out dataBaseApi);
            if (dataBaseApi == null)
            {
                var dbcontext = _serviceProvider.GetRequiredService<TDbContext>();
                dataBaseApi= new DataBaseApi(dbcontext);
                _databaseApi.Add(dbcontextName, dataBaseApi);
            }
            return (TDbContext)((DataBaseApi)dataBaseApi)._dbContext;
        }

    }
}
