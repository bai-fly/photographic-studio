using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Utils
{
    public class ServiceLocator
    {
        public static IServiceProvider Instance { get; set; }
    }
}
