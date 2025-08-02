namespace HubPixel.Domain.Entity;
public class MediaSource
{
    public string Name { get; private set; }
    public string Path { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }
    private List<Channel> _channels = [];
    public IReadOnlyCollection<Channel> Channels => _channels.AsReadOnly();
    public MediaSource(string name, string path)
    {
        Name = name;
        Path = path;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public void Update(string name, string path)
    {
        Name = name;
        Path = path;
        UpdatedAt = DateTime.UtcNow;
        Validate();
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
        Validate();
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
        Validate();
    }

    public void AddChannel(Channel channel)
    {
        if (channel == null)
            throw new ArgumentNullException(nameof(channel), "Channel cannot be null.");
        if (_channels.Any(c => c.Id == channel.Id))
            throw new InvalidOperationException("Channel already exists in the media source.");
        _channels.Add(channel);
    }

    public void RemoveChannel(Channel channel)
    {
        if (channel == null)
            throw new ArgumentNullException(nameof(channel), "Channel cannot be null.");
        if (!_channels.Remove(channel))
            throw new InvalidOperationException("Channel does not exist in the media source.");
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentException("Name cannot be empty.");
        if (string.IsNullOrWhiteSpace(Path))
            throw new ArgumentException("Path cannot be empty.");
    }
}
