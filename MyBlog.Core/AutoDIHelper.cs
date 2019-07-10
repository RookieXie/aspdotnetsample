using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyBlog.Core
{
    public static class AutoDIHelper
    {
        public static void AddAutoScope(this IServiceCollection services)
        {
            Assembly assembly = Assembly.Load("MyBlog.Services");
            var baseType = typeof(IAutoInjectScope);
            var types = assembly.GetExportedTypes().Where(t => baseType.IsAssignableFrom(t)).ToList();
            foreach (var item in types.Where(t => !t.IsInterface))
            {
                var interfaceType = item.GetInterfaces().Where(a => a != baseType).FirstOrDefault();
                services.AddScoped(interfaceType, item);
            }
        }
    }
}
