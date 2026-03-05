namespace IP.Shared.Abstractions.Extensions;

public static class AssemblyExtensions
{
    public static Type FindDerivedType(this Assembly assembly, Type baseType) =>
        assembly.GetTypes()
            .Single(t => t != baseType &&
                baseType.IsAssignableFrom(t) && t.IsInterface);

    public static IEnumerable<Type> FindDerivedTypes(this Assembly assembly, Type baseType) =>
        assembly.GetTypes()
            .Where(t => t != baseType &&
                baseType.IsAssignableFrom(t) && t.IsInterface);
}