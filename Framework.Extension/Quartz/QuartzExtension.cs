namespace Framework.Extensions
{
    /// <summary>
    /// 注册Quartz服务，待完善
    /// </summary>
    public static class QuartzExtension
    {
        public static void AddQuartz(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
        }
    }
}
