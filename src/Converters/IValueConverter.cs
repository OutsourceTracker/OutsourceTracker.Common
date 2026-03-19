namespace OutsourceTracker.Converters;

/// <summary>
/// Defines a mechanism for converting values between two types.
/// </summary>
/// <remarks>Implement this interface to provide custom logic for converting values between two types, such as for
/// serialization, data binding, or type adaptation scenarios. Implementations should ensure that conversions are
/// consistent and, where possible, reversible.</remarks>
/// <typeparam name="T0">The source type to convert from or to.</typeparam>
/// <typeparam name="T1">The target type to convert to or from.</typeparam>
public interface IValueConverter<T0, T1>
{
    /// <summary>
    /// Converts the specified object to an instance of type T1.
    /// </summary>
    /// <param name="obj">The object of type T0 to convert. Cannot be null.</param>
    /// <returns>An instance of type T1 that represents the converted value of the input object.</returns>
    T1 ConvertTo(T0 obj);

    /// <summary>
    /// Converts the specified object to an instance of type T0.
    /// </summary>
    /// <param name="obj">The object of type T1 to convert. Cannot be null unless the implementation supports null values.</param>
    /// <returns>An instance of type T0 that represents the converted value of the input object.</returns>
    T0 ConvertFrom(T1 obj);
}
