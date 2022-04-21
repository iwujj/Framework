try
{


    var builder = WebApplication.CreateBuilder(args);
    //配置aotuFac
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).
    ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacModuleRegister());//注册其他服务
    builder.RegisterAssemblyTypes(typeof(Program).Assembly)//注册控制器
                    .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t != typeof(ControllerBase))
                    .PropertiesAutowired();
    }).ConfigureLogging((hostingContext, builder) =>//日志
    {
        builder.AddFilter("System", LogLevel.Error);
        builder.AddFilter("Microsoft", LogLevel.Error);
        builder.SetMinimumLevel(LogLevel.Error);
        builder.AddLog4Net(Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config"));
    });


    //注册配置文件读取服务
    builder.Services.AddSingleton(new AppSettings(builder.Configuration));
    //注册缓存 给性能分析器使用
    builder.Services.AddCustomMemoryCache();
    //注册自定义跨域
    builder.Services.AddCustomCors();
    //注册性能分析器
    builder.Services.AddCustomProfiler();
    //注册接口文档Sagger
    builder.Services.AddCustomSwagger();
    //注册quartz任务调度器(待完善)
    builder.Services.AddQuartz();
    //注册http相关服务
    builder.Services.AddCustomHttpContext();
    //授权
    builder.Services.AddCustomAuthorization();
    //鉴权
    builder.Services.AddJwtAuthentication();
    //Ip限流
    builder.Services.AddIpRateLimiting(builder.Configuration);
    //添加数据库上下文
    builder.Services.AddDbContext<FrameworkContext>();
    //兼容同步请求
    builder.Services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
            .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);
    builder.Services.AddSession();
    builder.Services.AddControllers(o =>
    {
        o.ModelBinderProviders.Insert(0, new JobjectModelBinderProvider());
        o.Filters.Add(typeof(ApiActionFilterAttribute));
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

    var app = builder.Build();
    //swagger 文档
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(AppSettings.app(new string[] { "Cors", "PolicyName" }));
    //ip限流
    app.UseIpLimitMiddleware();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseExceptionHandlerMiddleware();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapGet("/helpdb", async context =>
        {
            var dbg = new DataBaseDictionaryGenerator(builder.Configuration.GetConnectionString("Default"));
            var html = dbg.ExportToHtml();
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync(html);

        });
    //endpoints.MapHub<ChatHub>("/api2/chatHub");
});



    app.Run();
}catch (Exception ex)
{
   
}
