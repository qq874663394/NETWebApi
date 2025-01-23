using Microsoft.Extensions.DependencyInjection;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IServices;
using WebApi.Domain.Interface.IServices.WebApiDB;
using WebApi.Domain.Services.WebApiDB;
using WebApi.Repositories.WebApiDB;

namespace WebApi.Domain.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            // 注册具体的服务实现类 UserService
            services.AddTransient<IUserServices, UserService>();
            services.AddTransient<IRoleServices, RoleService>();
            services.AddTransient<IOrgServices, OrgService>();
            services.AddTransient<IAuthServices, AuthService>();
            return services;
        }
    }
}