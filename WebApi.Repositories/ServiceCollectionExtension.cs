using Microsoft.Extensions.DependencyInjection;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IRepositories.WebApiDB;
using WebApi.Domain.Interface.IServices;
using WebApi.Repositories.WebApiDB;

namespace WebApi.Repositories
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWebApiRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork<>), typeof(BaseUnitOfWork<>));
            services.AddTransient<WebApiDbContext>();
            services.AddTransient<WebApiRepositoryContext>();
            services.AddTransient(p => new Lazy<WebApiDbContext>(p.GetRequiredService<WebApiDbContext>));
            services.AddTransient(p => new Lazy<WebApiRepositoryContext>(p.GetRequiredService<WebApiRepositoryContext>));
            services.AddTransient(typeof(IRepository<>), typeof(WebApiRepository<>));

            //Ioc UnitOfWork
            services.AddTransient<IUserUnitOfWork, UserUnitOfWork>();
            services.AddTransient<IRoleUnitOfWork, RoleUnitOfWork>();
            services.AddTransient<IOrgUnitOfWork, OrgUnitOfWork>();

            return services;
        }
    }
}
