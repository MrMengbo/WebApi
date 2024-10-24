using Microsoft.EntityFrameworkCore;
using Sample.GameManagement.Contracts;
using System.Runtime.CompilerServices;
using Zhaoxi.GameManagement.EntityFramework;

namespace Zhaoxi.GameManagement.HttpApi.Extensions
{
    public static class ServiceExtensions
    {
        // 定义静态的方法用于跨域Service，在program.cs中调用
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                // 增加跨域策略
                options.AddPolicy("AnyPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod() //允许任何HTTP方法
                        .AllowAnyHeader() //允许任何请求头
                        .AllowCredentials();//允许发送凭据（如cookies和HTTP 认证）
                    });
            });
        }

        // 在此服务的扩展类中配置编写Mysql的上下文代码
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("GameDb");
            services.AddDbContext<GameManagementDbContext>(
                builder => builder.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion));
        }

        //在扩展类中编写 包装器的注入
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

    }
}