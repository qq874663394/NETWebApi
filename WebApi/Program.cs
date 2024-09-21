using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Domain.Interface.IServices;
using WebApi.Domain.Services;
using WebApi.Filters;
using WebApi.Repositories;
using WebApi.Repositories.WebApiDB;
using WebApi.Utilities;

var builder = WebApplication.CreateBuilder(args);
//��ȡappsetting.json
var configuration = builder.Configuration;
//���ÿɿ���
builder.Services.AddCors();
//��ȡ���ݿ�����
builder.Services.AddDbContext<WebApiDbContext>((serviceProvider, options) =>
{
    options.UseSqlServer(configuration["ConnectionStrings:ConnectionString_SqlServer"]);
});
//ע��swagger
builder.Services.AddSwaggerGen(options =>// ���ǿ��ӻ��ӿ��ĵ�����
{
    // ���滹�кܶ�����ã�������Կ���΢��ٷ��ĵ�����swagger�ĵ�  ΢��https://docs.microsoft.com/zh-cn/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
    //����swagger��Ȩ���ð�ť
    var security = new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Description = "���¿�����������ͷ����Ҫ���Jwt��ȨToken��Bearer Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                Reference = new OpenApiReference()
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme,
                }
            }, Array.Empty<string>()
        }
    };
    options.AddSecurityRequirement(security);//���һ�������ȫ�ְ�ȫ��Ϣ����AddSecurityDefinition����ָ���ķ�������Ҫһ�£�������Bearer��

    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) �����ṹ: \"Authorization: Bearer {token}\"",
        Name = "Authorization",//jwtĬ�ϵĲ�������
        In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
        Type = SecuritySchemeType.ApiKey
    });
    // ����������Ŀ�е� XML ע���ļ�
    var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, "WebApi.xml");
    options.IncludeXmlComments(xmlCommentsPath);
    // ����������Ŀ�е� XML ע���ļ�
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WebApi.Controllers.xml"));
});
//ע��API Explorer
builder.Services.AddEndpointsApiExplorer();
//ע��ִ���
builder.Services.AddWebApiRepositories();
//ע����������
var services = builder.Services.AddDomainService();
//������Ϣע�뵽JwtHelper
builder.Services.AddSingleton(configuration);

//ע�������֤����ע��JWT
// ���������ṩ����
var myService = services.BuildServiceProvider().GetRequiredService<IAuthServices>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,//�Ƿ���֤ǩ��,����֤�Ļ����Դ۸����ݣ�����ȫ
        IssuerSigningKey = new SymmetricSecurityKey(myService.ConvertToByteArray(configuration["Jwt:SecretKey"])), //���ܵ���Կ
        ValidateIssuer = true,//�Ƿ���֤�����ˣ�������֤�غ��е�Iss�Ƿ��ӦValidIssuer����
        ValidIssuer = configuration["Jwt:Issuer"], //������Issuer
        ValidateAudience = true,//�Ƿ���֤�����ˣ�������֤�غ��е�Aud�Ƿ��ӦValidAudience����
        ValidAudience = configuration["Jwt:Audience"], //������Audience
        ValidateLifetime = true,//�Ƿ���֤����ʱ�䣬�����˾;ܾ�����
        ClockSkew = TimeSpan.FromSeconds(30), //����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ�����⣨�룩
        RequireExpirationTime = true,
    };
});
//ע�������
builder.Services.AddControllers();
services.AddScoped<JwtTokenFilterAttribute>();
//ע��API�쳣��������
//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add(typeof(ApiExceptionFilter));
//});

var app = builder.Build();
app.UseDeveloperExceptionPage();
// ���� Swagger UI�����ڿ��������У�
app.UseRouting();
app.UseCors("any");

//�����м����UseAuthentication����֤����������������Ҫ�����֤���м��ǰ���ã����� UseAuthorization����Ȩ����
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();// ����swagger�м��
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));// ��swaggerui������м������

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();