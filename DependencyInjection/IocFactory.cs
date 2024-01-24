using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace WebApi.DependencyInjection
{
    public static class IocFactory
    {
        private static object _locker = new object();
        private static IServiceCollection _services = null;
        private static IServiceProvider _provider = null;

        public static IServiceProvider GetServiceProvider()
        {
            lock (_locker)
            {
                if (_provider == null)
                {
                    if (_services == null)
                        _services = new ServiceCollection();
                    else
                        _provider = _services.BuildServiceProvider();
                }
            }

            return _provider;
        }

        public static IServiceCollection GetServices()
        {
            lock (_locker)
            {
                if (_services == null) _services = new ServiceCollection();
            }

            return _services;
        }

        public static void SetServices(IServiceCollection services)
        {
            lock (_locker)
            {
                _services = services;
            }
        }

        public static T GetService<T>()
        {
            T obj = default(T);

            lock (_locker)
            {
                obj = GetServiceProvider().GetService<T>();
            }

            return obj;
        }

        public static IEnumerable<T> GetServices<T>()
        {
            IEnumerable<T> obj = default(IEnumerable<T>);

            lock (_locker)
            {
                obj = GetServiceProvider().GetServices<T>();
            }
            return obj;
        }

        /// <summary>
        /// 注册控制反转相关内容
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIoc(this IServiceCollection services)
        {
            SetServices(services);

            return services;
        }
    }
}