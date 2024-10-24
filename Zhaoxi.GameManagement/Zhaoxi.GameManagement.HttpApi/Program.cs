using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Zhaoxi.GameManagement.HttpApi.Extensions;


// 1.创建Web应用的配置对象builder
var builder = WebApplication.CreateBuilder(args);

// 2.添加服务到容器
// AddControllers()方法用于注册MVC控制器所需的服务
builder.Services.AddControllers();

// AddEndpointsApiExplorer() 方法注册了一个服务，用于发现和分析API端点
builder.Services.AddEndpointsApiExplorer();

// AddSwaggerGen()方法用于注册Swagger生成器，它可以根据你的API生成Swagger文档
builder.Services.AddSwaggerGen();

// ConfigureCors（）注册跨域服务
//builder.Services.ConfigureCors();

//ConfigureMysqlContext()方法将数据库服务注册一下。
builder.Services.ConfigureMySqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryWrapper();

// 实现模型类到那个啥的映射文件咯芜
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//添加跨域策略，打通前后端。
//app.UseCors("AnyPolicy");

// app.UseAuthorization() 方法用于启用授权中间件。这是执行身份验证和授权检查的必要步骤。
app.UseAuthorization();

// app.MapControllers() 方法用于将 HTTP 请求映射到控制器。
app.MapControllers();

app.Run();