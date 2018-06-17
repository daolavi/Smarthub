using Autofac;

namespace System.Web.Mvc
{
    /// <summary>
    /// We use this service to resolve the dependencies outside of the MVC assembly
    /// </summary>
    public class MvcDependencyResolver : ISmartHubDependencyResolver
    {
        private static Lazy<MvcDependencyResolver> instance;

        private IDependencyResolver internalResolver;
        private IContainer internalContainer;

        public MvcDependencyResolver(IDependencyResolver resolver, IContainer container)
        {
            internalResolver = resolver;
            internalContainer = container;
        }

        #region Static props
        public static MvcDependencyResolver Current { get { return instance.Value; } }

        public static void SetResolver(IDependencyResolver resolver, IContainer container) { instance = new Lazy<MvcDependencyResolver>(() => new MvcDependencyResolver(resolver, container)); }
        #endregion

        public T GetService<T>()
        {
            return internalResolver.GetService<T>();
        }

        public object GetService(Type type)
        {
            return internalResolver.GetService(type);
        }

        public T GetNamedService<T>(string name)
        {
            return internalContainer.ResolveNamed<T>(name);
        }
    }
}
