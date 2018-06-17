namespace System
{
    public class ResolvedForAttribute : Attribute
    {
        public ResolvedForAttribute(Type forInterface, string named = null, bool singleton = false)
        {
            ForInterface = forInterface;
            Named = named;
            Singleton = singleton;
        }

        public Type ForInterface { get; private set; }

        public bool Singleton { get; private set; }

        public string Named { get; private set; }
    }
}
