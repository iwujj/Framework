

var builder = WebApplication.CreateBuilder(args);
//����aotuFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).
ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacModuleRegister());
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
//������
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
