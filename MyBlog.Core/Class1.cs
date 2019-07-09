using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace MyBlog.Core
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class AutowiredAttribute : Attribute, IServiceProviderFactory<IServiceProvider>
    {
        public IServiceProvider CreateBuilder(IServiceCollection services) => throw new NotImplementedException();
        public IServiceProvider CreateServiceProvider(IServiceProvider containerBuilder) => containerBuilder;

    }
}
