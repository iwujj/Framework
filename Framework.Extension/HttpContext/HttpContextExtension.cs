namespace Framework.Extensions
{
    public static class HttpContextExtension
    {
        public static void AddCustomHttpContext(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, CurrentUser>();
        }
    }
}
