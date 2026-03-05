namespace IP.Shared.Abstractions.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value) =>
         value.GetAttribute<DescriptionAttribute>()?
            .Description ?? string.Empty;

    private static T GetAttribute<T>(this Enum value) where T : Attribute
    {
        return (T)value.GetType()
            .GetField(
                value.ToString(),
                BindingFlags.Public | BindingFlags.Static)?
            .GetCustomAttributes(typeof(T), false).FirstOrDefault()!;
    }
}