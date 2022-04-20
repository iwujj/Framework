

var builder = WebApplication.CreateBuilder(args);
//配置aotuFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).
ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacModuleRegister());
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
//控制器
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    //endpoints.MapHub<ChatHub>("/api2/chatHub");
});

app.Run();
