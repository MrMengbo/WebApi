using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Zhaoxi.GameManagement.HttpApi.Extensions;


// 1.����WebӦ�õ����ö���builder
var builder = WebApplication.CreateBuilder(args);

// 2.��ӷ�������
// AddControllers()��������ע��MVC����������ķ���
builder.Services.AddControllers();

// AddEndpointsApiExplorer() ����ע����һ���������ڷ��ֺͷ���API�˵�
builder.Services.AddEndpointsApiExplorer();

// AddSwaggerGen()��������ע��Swagger�������������Ը������API����Swagger�ĵ�
builder.Services.AddSwaggerGen();

// ConfigureCors����ע��������
//builder.Services.ConfigureCors();

//ConfigureMysqlContext()���������ݿ����ע��һ�¡�
builder.Services.ConfigureMySqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryWrapper();

// ʵ��ģ���ൽ�Ǹ�ɶ��ӳ���ļ�����
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//��ӿ�����ԣ���ͨǰ��ˡ�
//app.UseCors("AnyPolicy");

// app.UseAuthorization() ��������������Ȩ�м��������ִ�������֤����Ȩ���ı�Ҫ���衣
app.UseAuthorization();

// app.MapControllers() �������ڽ� HTTP ����ӳ�䵽��������
app.MapControllers();

app.Run();