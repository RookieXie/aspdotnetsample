using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyBlog.Core
{
    public static class ExtensionMethod
    {
        public static IServiceProvider Autowired(this IServiceProvider serviceProvider, object instance)
        {
            if (serviceProvider == null || instance == null)
            {
                return serviceProvider;
            }

            var flags = BindingFlags.Public | BindingFlags.NonPublic;
            var type = instance as Type ?? instance.GetType();
            if (instance is Type)
            {
                instance = null;
                flags |= BindingFlags.Static;
            }
            else
            {
                flags |= BindingFlags.Instance;
            }

            // 字段
            foreach (var field in type.GetFields(flags))
            {
                var attr = field.GetCustomAttributes().OfType<IServiceProviderFactory<IServiceProvider>>().LastOrDefault();
                var value = attr?.CreateServiceProvider(serviceProvider).GetServiceOrCreateInstance(field.FieldType);
                if (value != null)
                {
                    field.SetValue(instance, value);
                }
            }

            foreach (var property in type.GetProperties(flags))
            {
                var attr = property.GetCustomAttributes().OfType<IServiceProviderFactory<IServiceProvider>>().LastOrDefault();
                var value = attr?.CreateServiceProvider(serviceProvider).GetServiceOrCreateInstance(property.PropertyType);
                if (value != null)
                {
                    var setter = property.GetSetMethod(true);
                    if (setter != null)
                    {   // 一般属性
                        setter.Invoke(instance, new[] { value });
                    }
                    else if (type.GetField($"<{property.Name}>k__BackingField", flags) is FieldInfo field)
                    {   // 只读属性
                        field.SetValue(instance, value);
                    }
                }
            }

            return serviceProvider;
        }
        public static object GetServiceOrCreateInstance(this IServiceProvider serviceProvider, Type type)
        {
            if (serviceProvider == null)
            {
                return Activator.CreateInstance(type);
            }

            var obj = ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, type);
            if (obj != null)
            {
                serviceProvider.Autowired(obj);
            }
            return obj;
        }


        public static IApplicationBuilder UseRequestLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
