namespace Framework.Extensions
{
    public static class IpRateLimitingMiddleware
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(IpRateLimitingMiddleware));
        public static void UseIpLimitMiddleware(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            try
            {
                if (AppSettings.app("AppSettings", "UseIpRateLimit").ObjToBool())
                {
                    app.UseIpRateLimiting();
                }
            }
            catch (Exception e)
            {
                Log.Error($"Error occured limiting ip rate.\n{e.Message}");
                throw;
            }
        }

    }
}
