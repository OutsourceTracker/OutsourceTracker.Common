namespace OutsourceTracker.Converters;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
public class DatabaseConverterAttribute : Attribute
{
    public Type ConverterType { get; }

    public bool EFOnly { get; set; } = false;

    public DatabaseConverterAttribute(Type converterType)
    {
        ConverterType = converterType;
    }
}
