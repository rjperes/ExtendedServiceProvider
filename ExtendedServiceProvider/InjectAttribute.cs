namespace ExtendedServiceProvider
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class InjectAttribute : Attribute
    {
        public object? ServiceKey { get; init; }
    }
}
