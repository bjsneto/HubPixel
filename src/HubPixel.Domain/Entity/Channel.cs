using HubPixel.Domain.SeedWork;

namespace HubPixel.Domain.Entity;
public class Channel : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public UrlStream UrlStream { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<Guid> _categoryIds = new();
    public IReadOnlyCollection<Guid> CategoryIds => _categoryIds.AsReadOnly();

    private Channel(string name, string description, UrlStream urlStream, IEnumerable<Guid> categoryIds)
        : base()
    {
        Name = name;
        Description = description;
        UrlStream = urlStream;
        CreatedAt = DateTime.UtcNow;
        _categoryIds.AddRange(categoryIds ?? []);
    }

    public static Channel Create(string name, string description, string urlStream, IEnumerable<Guid> categoryIds)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.");

        var url = UrlStream.Create(urlStream);

        return new Channel(name, description, url, categoryIds);
    }

    public void Update(string name, string description, string urlStream)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.");

        Name = name;
        Description = description;
        UrlStream = UrlStream.Create(urlStream);
    }

    public void AddCategories(IEnumerable<Guid> newCategoryIds)
    {
        if (newCategoryIds == null)
            throw new ArgumentNullException(nameof(newCategoryIds));

        // Evita duplicatas
        var categoriesToAdd = newCategoryIds.Where(id => !_categoryIds.Contains(id));
        _categoryIds.AddRange(categoriesToAdd);
    }

    public void RemoveCategories(IEnumerable<Guid> categoryIdsToRemove)
    {
        ArgumentNullException.ThrowIfNull(categoryIdsToRemove);

        foreach (var id in categoryIdsToRemove)
        {
            _categoryIds.Remove(id);
        }
    }
}