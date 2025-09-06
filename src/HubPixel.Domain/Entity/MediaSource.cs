using HubPixel.Domain.SeedWork;

namespace HubPixel.Domain.Entity;
public class MediaSource : AggregateRoot
{
    public string Name { get; private set; }
    public string Path { get; private set; } 
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    private readonly List<Guid> _channelIds = new();
    public IReadOnlyCollection<Guid> ChannelIds => _channelIds.AsReadOnly();

    private MediaSource(string name, string path)
        : base()
    {
        Name = name;
        Path = path;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public static MediaSource Create(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be empty.", nameof(path));

        return new MediaSource(name, path);
    }

    public void Update(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be empty.", nameof(path));

        Name = name;
        Path = path;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddChannel(Guid channelId)
    {
        if (channelId == Guid.Empty)
            throw new ArgumentException("Channel ID cannot be empty.", nameof(channelId));
        if (_channelIds.Contains(channelId))
            throw new InvalidOperationException("Channel already exists in the media source.");

        _channelIds.Add(channelId);
    }

    public void RemoveChannel(Guid channelId)
    {
        if (channelId == Guid.Empty)
            throw new ArgumentException("Channel ID cannot be empty.", nameof(channelId));
        if (!_channelIds.Remove(channelId))
            throw new InvalidOperationException("Channel does not exist in the media source.");
    }
}
