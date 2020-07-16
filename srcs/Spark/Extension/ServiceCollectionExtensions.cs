using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Spark.Core.Extension;
using Spark.Game.Abstraction.Factory;
using Spark.Game.Factory;
using Spark.Network.Session;
using ObjectFactory = Spark.Game.Factory.ObjectFactory;

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

        public static void AddGameFactories(this IServiceCollection services)
        {
            services.AddTransient<IMapFactory, MapFactory>();
            services.AddTransient<ISkillFactory, SkillFactory>();
            services.AddTransient<IEntityFactory, EntityFactory>();
            services.AddTransient<ISessionFactory, SessionFactory>();
            services.AddTransient<IObjectFactory, ObjectFactory>();
            services.AddTransient<IBuffFactory, BuffFactory>();
            services.AddTransient<IPortalFactory, PortalFactory>();
        }
    }
}