using HubPixel.Domain.SeedWork;

namespace HubPixel.Domain.Entity;
public class ChannelStatus : AggregateRoot
{
    public Guid ChannelId { get; private set; }
    public bool IsOnline { get; private set; }
    public DateTime LastCheckedAt { get; private set; }

    private ChannelStatus(Guid channelId, bool isOnline)
        : base()
    {
        ChannelId = channelId;
        IsOnline = isOnline;
        LastCheckedAt = DateTime.UtcNow;
    }

    public static ChannelStatus Create(Guid channelId, bool isOnline)
    {
        if (channelId == Guid.Empty)
        {
            throw new ArgumentException("Channel ID cannot be empty.", nameof(channelId));
        }

        return new ChannelStatus(channelId, isOnline);
    }

    public void UpdateStatus(bool isOnline)
    {
        if (IsOnline != isOnline)
        {
            IsOnline = isOnline;
            LastCheckedAt = DateTime.UtcNow;
        }
    }
}