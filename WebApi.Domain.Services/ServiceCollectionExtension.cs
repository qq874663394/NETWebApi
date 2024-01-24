using Microsoft.Extensions.DependencyInjection;
using WebApi.Domain.Interface.IServices.WebApiDB;
using WebApi.Domain.Services.WebApiDB;

namespace WebApi.Domain.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            services.AddTransient<IRelevanceService, RelevanceService>();
            return services;
        }
    }
}