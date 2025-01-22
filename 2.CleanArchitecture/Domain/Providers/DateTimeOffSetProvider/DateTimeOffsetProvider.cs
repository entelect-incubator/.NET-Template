namespace Domain.Providers.DateTimeOffSetProvider;
public abstract class DateTimeOffsetProvider
{
    private static DateTimeOffsetProvider current =
        DefaultDateTimeOffsetProvider.Instance;

    public static DateTimeOffsetProvider Current
    {
        get => current;
        set => current = value ?? throw new ArgumentNullException("value");
    }

    public abstract DateTimeOffset Now { get; }

    public abstract string NowAsString { get; }

    public static void ResetToDefault() => current = DefaultDateTimeOffsetProvider.Instance;
}
