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
}
