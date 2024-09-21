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
//读取appsetting.json
var configuration = builder.Configuration;
//设置可跨域
builder.Services.AddCors();
//读取数据库配置
builder.Services.AddDbContext<WebApiDbContext>((serviceProvider, options) =>
{
    options.UseSqlServer(configuration["ConnectionStrings:ConnectionString_SqlServer"]);
});
//注册swagger
builder.Services.AddSwaggerGen(options =>// 我们可视化接口文档服务
{
    // 里面还有很多的配置，具体可以看看微软官方文档或者swagger文档  微软：https://docs.microsoft.com/zh-cn/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
    //开启swagger授权设置按钮
    var security = new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
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
    options.AddSecurityRequirement(security);//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。

    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
        Name = "Authorization",//jwt默认的参数名称
        In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
        Type = SecuritySchemeType.ApiKey
    });
    // 引用其他项目中的 XML 注释文件
    var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, "WebApi.xml");
    options.IncludeXmlComments(xmlCommentsPath);
    // 引用其他项目中的 XML 注释文件
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WebApi.Controllers.xml"));
});
//注册API Explorer
builder.Services.AddEndpointsApiExplorer();
//注入仓储层
builder.Services.AddWebApiRepositories();
//注入领域服务层
var services = builder.Services.AddDomainService();
//配置信息注入到JwtHelper
builder.Services.AddSingleton(configuration);

//注册身份验证服务，注册JWT
// 构建服务提供程序
var myService = services.BuildServiceProvider().GetRequiredService<IAuthServices>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,//是否验证签名,不验证的画可以篡改数据，不安全
        IssuerSigningKey = new SymmetricSecurityKey(myService.ConvertToByteArray(configuration["Jwt:SecretKey"])), //解密的密钥
        ValidateIssuer = true,//是否验证发行人，就是验证载荷中的Iss是否对应ValidIssuer参数
        ValidIssuer = configuration["Jwt:Issuer"], //发行人Issuer
        ValidateAudience = true,//是否验证订阅人，就是验证载荷中的Aud是否对应ValidAudience参数
        ValidAudience = configuration["Jwt:Audience"], //订阅人Audience
        ValidateLifetime = true,//是否验证过期时间，过期了就拒绝访问
        ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
        RequireExpirationTime = true,
    };
});
//注册控制器
builder.Services.AddControllers();
services.AddScoped<JwtTokenFilterAttribute>();
//注册API异常过滤器类
//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add(typeof(ApiExceptionFilter));
//});

var app = builder.Build();
app.UseDeveloperExceptionPage();
// 启用 Swagger UI（仅在开发环境中）
app.UseRouting();
app.UseCors("any");

//调用中间件：UseAuthentication（认证），必须在所有需要身份认证的中间件前调用，比如 UseAuthorization（授权）。
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();// 启用swagger中间件
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));// 对swaggerui界面的中间件启用

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();