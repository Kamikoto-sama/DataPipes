namespace Sandbox;

public static class Extensions
{
    public static IEnumerable<(T Item, int Index)> Enumerate<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (item, index));
    }

    public static bool Implements(this Type type, Type interfaceType)
    {
        return type
            .GetInterfaces()
            .Any(i => i == interfaceType || (i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType));
    }
}