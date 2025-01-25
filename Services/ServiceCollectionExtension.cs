using Microsoft.Extensions.DependencyInjection;
using Domain.Entities;
using Domain.Interface.IRepositories;
using Domain.Interface.IServices;
using Domain.Interface.IServices.WebApiDB;
using Domain.Services.WebApiDB;
using Repositories.WebApiDB;

namespace Domain.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            // 注册具体的服务实现类 UserService
            services.AddTransient<IMenuServices, MenuService>();
            services.AddTransient<IOrgServices, OrgService>();
            services.AddTransient<IRoleServices, RoleService>();
            services.AddTransient<IUserServices, UserService>();
            services.AddTransient<IAuthServices, AuthService>();
            return services;
        }
    }
}