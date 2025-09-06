using HubPixel.Domain.SeedWork;

namespace HubPixel.Domain.Entity;
public class Category : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Category(string name, string description)
        : base()
    {
        Name = name;
        Description = description;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public static Category Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.");
        if (name.Length > 100)
            throw new ArgumentException("Name cannot exceed 100 characters.");

        return new Category(name, description);
    }

    public void Update(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.");

        Name = name;
        Description = description;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}
