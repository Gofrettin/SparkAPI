using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Spark.Core.Extension;

namespace Spark.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddImplementingTypes<T>(this IServiceCollection services)
        {
            IEnumerable<Type> types = typeof(T).Assembly.GetImplementingTypes<T>();
            foreach (Type type in types)
            {
                services.AddTransient(typeof(T), type);
            }
        }
    }
}