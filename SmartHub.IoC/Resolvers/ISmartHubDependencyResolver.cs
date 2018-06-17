namespace System
{
    public interface ISmartHubDependencyResolver
    {
        T GetService<T>();

        T GetNamedService<T>(string name);

        object GetService(Type type);
    }
}
