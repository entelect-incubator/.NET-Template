namespace Common.Providers.GuidProvider;
public abstract class GuidProvider
{
    private static GuidProvider current =
        DefaultGuidProvider.Instance;

    public static GuidProvider Current
    {
        get => current;
        set => current = value ?? throw new ArgumentNullException("value");
    }

    public abstract Guid NewGuid { get; }

    public abstract string NewGuidAsString { get; }

    public static void ResetToDefault() => current = DefaultGuidProvider.Instance;
}

public class DefaultGuidProvider : GuidProvider
{
    private DefaultGuidProvider()
    {
    }

    public static DefaultGuidProvider Instance { get; } = new();

    public override Guid NewGuid => Guid.NewGuid();

    public override string NewGuidAsString => Guid.NewGuid().ToString();
}
