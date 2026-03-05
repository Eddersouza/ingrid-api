namespace IP.Shared.Abstractions;

public class AssemblyReference :
   AssemblyReferenceBase<AssemblyReference>;

public class AssemblyReferenceBase<TClass> where TClass : class
{
    public static Assembly Assembly => typeof(TClass).Assembly;

    public static string Name => Assembly.GetName().Name ?? string.Empty;
}