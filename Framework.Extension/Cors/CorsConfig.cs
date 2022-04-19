namespace Framework.Extensions
{
    /// <summary>
    /// 配置跨服服务
    /// </summary>
    public static class CorsConfig
    {
        public static void AddCustomCors(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddCors(c =>
            {
                if (!AppSettings.app(new string[] {"Cors", "AllowAll" }).ObjToBool())
                {
                    c.AddPolicy(AppSettings.app(new string[] { "Cors", "PolicyName" }),

                        policy =>
                        {

                            policy
                            .WithOrigins(AppSettings.app(new string[] { "Cors", "IPs" }).Split(','))
                            .AllowAnyHeader()//Ensures that the policy allows any header.
                            .AllowAnyMethod();
                        });
                }
                else
                {
                    //允许任意跨域请求
                    c.AddPolicy(AppSettings.app(new string[] { "Cors", "PolicyName" }),
                        policy =>
                        {
                            policy
                            .SetIsOriginAllowed((host) => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                        });
                }

            });
        }
    }
}
