namespace IP.Shared.Abstractions.Extensions;

public static class ExceptionExtension
{
    public static string GetAllMessages(this Exception exception) =>
        string.Join(
            Environment.NewLine,
            exception.FromHierarchy(ex => ex.InnerException!)
                .Select(ex => ex.Message));

    public static string GetAllStackTraces(this Exception exception) =>
        string.Join(
            Environment.NewLine,
            exception.FromHierarchy(ex => ex.InnerException!)
                .Select(ex => ex.StackTrace));

    public static string GetInnerExceptions(this Exception exception) =>
        string.Join(
            Environment.NewLine,
            exception.FromHierarchy(ex => ex.InnerException!)
                .Select(ex =>
                {
                    StringBuilder sb = new();

                    sb.AppendLine($"Message = {ex.Message};");
                    sb.AppendLine($"StackTrace = {ex.StackTrace};");

                    return sb.ToString();
                }));
}