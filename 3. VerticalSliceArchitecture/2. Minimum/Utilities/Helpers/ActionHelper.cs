namespace Common.Helpers;

public static class ActionHelper
{
    public static void Apply(Action action, bool condition = true)
    {
        if (condition)
        {
            action();
        }
    }

    public static void UpdateIf(Action action, object? property)
    {
        if (property is string stringValue && !string.IsNullOrEmpty(stringValue))
        {
            action();
        }
        else if (IsValueType(property))
        {
            action();
        }
    }

    public static bool IsValueType(object? obj)
        => obj != null && obj.GetType().IsValueType;
}
