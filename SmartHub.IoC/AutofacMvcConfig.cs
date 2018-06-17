using Autofac.Core.Lifetime;
using Hangfire;

namespace System.Web.Mvc
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Linq;
    using Reflection;

    public static class AutofacMvcConfig
    {
        public static IContainer RegisterAll(Assembly mvcAssemblies, params Assembly[] smartHubAssemblies)
        {
            var builder = new ContainerBuilder();

            #region MVC registrations
            // Register your MVC controllers.
            builder.RegisterControllers(mvcAssemblies);

            // OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            //builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();
            #endregion

            #region SmartHub registrations
            // By naming convention IServiceName -> ServiceName
            builder.RegisterByNamingConvention(smartHubAssemblies);

            // By custom attribute
            // This only needs if the service and interface are not following the naming convention
            // Or named/keyed registration
            // Or singleton
            builder.RegisterByAttribute(smartHubAssemblies);
            #endregion

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);

            // Set the global resolver so anyone can use it without referencing the MVC project
            MvcDependencyResolver.SetResolver(resolver, container);

            return container;
        }

        private static void RegisterByNamingConvention(this ContainerBuilder builder, Assembly[] smartHubAssemblies)
        {
            var interfaces = smartHubAssemblies.SelectMany(x => x.GetTypes().Where(y => y.IsInterface));
            var concretes = smartHubAssemblies.SelectMany(x => x.GetTypes().Where(y => !y.IsInterface && !y.IsAbstract));
            foreach (var interfaceType in interfaces)
            {
                var concreteName = interfaceType.Name.Substring(1);
                var concreteType = concretes.FirstOrDefault(x => x.Name.Equals(concreteName));

                //Fail cases
                if (concreteType == null //Not found
                    || concreteType.GetInterfaces().All(x => x != interfaceType) // Not implementing the interface
                    || concreteType.GetCustomAttribute<ResolvedForAttribute>() != null) // Has the custom attribute
                    continue;

                builder.RegisterType(concreteType).As(interfaceType).InstancePerMatchingLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag, AutofacJobActivator.LifetimeScopeTag);
            }
        }

        private static void RegisterByAttribute(this ContainerBuilder builder, Assembly[] smartHubAssemblies)
        {
            var concretes = smartHubAssemblies.SelectMany(x => x.GetTypes().Where(y => y.GetCustomAttribute<ResolvedForAttribute>() != null)).ToArray();
            foreach (var concreteType in concretes)
            {
                var attr = concreteType.GetCustomAttribute<ResolvedForAttribute>();
                if (attr.Singleton)
                {
                    if (!string.IsNullOrWhiteSpace(attr.Named))
                    {
                        builder.RegisterType(concreteType).Keyed(attr.Named, attr.ForInterface).SingleInstance();
                    }
                    else
                    {
                        builder.RegisterType(concreteType).As(attr.ForInterface).SingleInstance();
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(attr.Named))
                    {
                        builder.RegisterType(concreteType).Keyed(attr.Named, attr.ForInterface).InstancePerMatchingLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag, AutofacJobActivator.LifetimeScopeTag);
                    }
                    else
                    {
                        builder.RegisterType(concreteType).As(attr.ForInterface).InstancePerMatchingLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag, AutofacJobActivator.LifetimeScopeTag);
                    }
                }
            }
        }
    }
}
