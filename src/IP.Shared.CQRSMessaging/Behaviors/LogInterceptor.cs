namespace IP.Shared.CQRSMessaging.Behaviors;

public static class LogInterceptor
{
    public static T MaskFields<T>(T obj)
    {
        if (EqualityComparer<T>.Default.Equals(obj, default)) return obj;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };
        var cloneJson = JsonSerializer.Serialize(obj, options);
        var clone = JsonSerializer.Deserialize<T>(cloneJson, options);

        
        if (EqualityComparer<T>.Default.Equals(clone, default)) return obj;


        MaskFieldsRecursive(clone!, []);

        return clone!;
    }

    private static bool CanMask(PropertyInfo prop) =>
        Attribute.IsDefined(prop, typeof(MaskFieldInLogAttribute)) &&
        prop.CanWrite;

    private static void MaskFieldsRecursive(object obj, HashSet<object> visited)
    {
        if (obj == null || visited.Contains(obj)) return;
        visited.Add(obj);

        var type = obj.GetType();
        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (CanMask(prop))
                MaskValue(obj, prop);
            else if (PropertyNotIndexed(prop))
            {
                var value = prop.GetValue(obj);
                if (value == null) continue;

                var propType = prop.PropertyType;
                if (PropertyIsClass(propType))
                    MaskFieldsRecursive(value, visited);
                else if (PropertyIsList(propType))
                    SearchInList(visited, value);
            }
        }
    }

    private static void MaskValue(object obj, PropertyInfo prop)
    {
        if (prop.PropertyType == typeof(string))
        {
            prop.SetValue(obj, "***************");
        }
        // Add other types as needed
    }

    private static bool PropertyIsClass(Type propType) =>
        propType.IsClass && propType != typeof(string);

    private static bool PropertyIsList(Type propType) =>
        typeof(System.Collections.IEnumerable).IsAssignableFrom(propType) &&
        propType != typeof(string);

    private static bool PropertyNotIndexed(PropertyInfo prop) =>
        prop.CanRead && prop.GetIndexParameters().Length == 0;

    private static void SearchInList(HashSet<object> visited, object value)
    {
        foreach (var item in (System.Collections.IEnumerable)value)
        {
            if (item != null && item.GetType().IsClass && item is not string)
            {
                MaskFieldsRecursive(item, visited);
            }
        }
    }
}