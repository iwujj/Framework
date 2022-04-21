try
{


    var builder = WebApplication.CreateBuilder(args);
    //����aotuFac
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).
    ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacModuleRegister());//ע����������
    builder.RegisterAssemblyTypes(typeof(Program).Assembly)//ע�������
                    .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t != typeof(ControllerBase))
                    .PropertiesAutowired();
    }).ConfigureLogging((hostingContext, builder) =>//��־
    {
        builder.AddFilter("System", LogLevel.Error);
        builder.AddFilter("Microsoft", LogLevel.Error);
        builder.SetMinimumLevel(LogLevel.Error);
        builder.AddLog4Net(Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config"));
    });


    //ע�������ļ���ȡ����
    builder.Services.AddSingleton(new AppSettings(builder.Configuration));
    //ע�Ỻ�� �����ܷ�����ʹ��
    builder.Services.AddCustomMemoryCache();
    //ע���Զ������
    builder.Services.AddCustomCors();
    //ע�����ܷ�����
    builder.Services.AddCustomProfiler();
    //ע��ӿ��ĵ�Sagger
    builder.Services.AddCustomSwagger();
    //ע��quartz���������(������)
    builder.Services.AddQuartz();
    //ע��http��ط���
    builder.Services.AddCustomHttpContext();
    //��Ȩ
    builder.Services.AddCustomAuthorization();
    //��Ȩ
    builder.Services.AddJwtAuthentication();
    //Ip����
    builder.Services.AddIpRateLimiting(builder.Configuration);
    //������ݿ�������
    builder.Services.AddDbContext<FrameworkContext>();
    //����ͬ������
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
    //swagger �ĵ�
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(AppSettings.app(new string[] { "Cors", "PolicyName" }));
    //ip����
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
