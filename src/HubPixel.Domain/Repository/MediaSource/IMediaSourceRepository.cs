using DomainEntity = HubPixel.Domain.Entity.MediaSource;

namespace HubPixel.Domain.Repository.MediaSource;

public interface IMediaSourceRepository
{
    Task AddAsync(DomainEntity mediaSource, CancellationToken cancellationToken);
}
