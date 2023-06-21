namespace Common.Providers.GuidProvider;

public interface IGuidService
{
    Guid NewGuid();

    string NewGuidAsString();
}
