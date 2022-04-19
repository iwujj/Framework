
namespace Framework.Extensions
{
    public static class SwaggerConfig
    {
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            var ProjectName = AppSettings.app(new string[] { "ProjectInfo", "Name" });
            var Email = AppSettings.app(new string[] { "ProjectInfo", "Eamil" });

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"{ProjectName} 接口文档——{RuntimeInformation.FrameworkDescription}",
                    Description = $"{ProjectName} Web API v1",
                    Contact = new OpenApiContact { Name = ProjectName, Email = Email, },
                    License = new OpenApiLicense { Name = ProjectName + "接口文档" }
                });
                c.OrderActionsBy(o => o.RelativePath);
                // Jwt Bearer 认证，
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });

            });
            services.AddSwaggerGenNewtonsoftSupport();

        }
    }
  
}
