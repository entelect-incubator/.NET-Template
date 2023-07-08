namespace Common.Providers.DateTimeOffSetProvider;
public class DefaultDateTimeOffsetProvider : DateTimeOffsetProvider
{
    private static readonly DefaultDateTimeOffsetProvider InstanceValue = new();

    private DefaultDateTimeOffsetProvider()
    {
    }

    public static DefaultDateTimeOffsetProvider Instance => InstanceValue;

    public override DateTimeOffset Now => DateTimeOffset.Now;

    public override string NowAsString => DateTimeOffset.Now.ToString("yyyyMMddHHmmssffff");
}
