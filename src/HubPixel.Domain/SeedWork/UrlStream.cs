namespace HubPixel.Domain.SeedWork;
public class UrlStream : ValueObject
{
    public string Value { get; private set; }

    private UrlStream(string value)
    {
        Value = value;
    }

    public static UrlStream Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Uri.TryCreate(value, UriKind.Absolute, out _))
        {
            throw new ArgumentException("Invalid URL stream format.");
        }
        return new UrlStream(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
