using HubPixel.Domain.SeedWork;

namespace HubPixel.Domain.Entity;
public class Channel : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string UrlStream { get; private set; }
    public DateTime CreatedAt { get; private set; }
    private List<Category> _categories;
    public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

    public Channel(string name, string description, string urlStream)
        : base()
    {
        Name = name;
        Description = description;
        UrlStream = urlStream;
        CreatedAt = DateTime.UtcNow;
        _categories = [];
        Validate();
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentException("Name cannot be empty.");
        if (string.IsNullOrWhiteSpace(UrlStream))
            throw new ArgumentException("UrlStream cannot be empty.");
    }

}
