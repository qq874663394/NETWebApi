using Microsoft.Extensions.DependencyInjection;
using Domain.Entities;
using Domain.Interface.IAggregateRoots;
using Domain.Interface.IRepositories;
using Domain.Interface.IRepositories.WebApiDB;
using Domain.Interface.IServices;
using Repositories.WebApiDB;
using Microsoft.Extensions.Logging;

namespace Repositories
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

            services.AddTransient<IMenuUnitOfWork, MenuUnitOfWork>();
            services.AddTransient<IOrgUnitOfWork, OrgUnitOfWork>();
            services.AddTransient<IRoleUnitOfWork, RoleUnitOfWork>();
            services.AddTransient<IUserUnitOfWork, UserUnitOfWork>();

            return services;
        }
    }
}
