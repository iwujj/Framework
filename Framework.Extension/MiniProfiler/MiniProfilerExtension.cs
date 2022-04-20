namespace Framework.Extensions
{
    public static class MiniProfilerExtension
    {
        public static void AddCustomProfiler(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddMiniProfiler(options =>
             {
                 options.RouteBasePath = "/profiler";
                 //(options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(10);
                 options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.Left;
                 options.PopupShowTimeWithChildren = true;
             });
        }
    }
}
