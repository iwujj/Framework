namespace Framework.Extensions
{
    public static class MemoryCacheExtension
    {
        public static void AddCustomMemoryCache(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<ICaching, MemoryCaching>();
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var value = factory.GetRequiredService<IOptions<MemoryCacheOptions>>();
                var cache = new MemoryCache(value);
                return cache;
            });
        }
    }
}
