namespace OutsourceTracker.Services.ModelService;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class IgnoreParameterAttribute : Attribute
{
}
