using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Utils
{
    public class IOC
    {
        private static ServiceProvider serviceProvider;
        public static void init(IServiceCollection services)
        {
            serviceProvider = services.BuildServiceProvider();
        }
        public static T GetService<T>()
        {
            return (T)serviceProvider.GetService(typeof(T));
        }
    }
}
