using HubPixel.Domain.Repository.MediaSource;
using DomainEntity = HubPixel.Domain.Entity.MediaSource;

namespace HubPixel.Application.MediaSource;
public class MediaSourceService : IMediaSourceService
{
    private readonly IMediaSourceRepository _mediaSourceRepository;

    public MediaSourceService(IMediaSourceRepository mediaSourceRepository)
    {
        _mediaSourceRepository = mediaSourceRepository;
    }

    public async Task CreateAsync(CreateMediaSourceInput input,
        CancellationToken cancellationToken)
    {
        var newMediaSource = DomainEntity.Create(input.Name, input.Path);
        await _mediaSourceRepository.AddAsync(newMediaSource, cancellationToken);
    }
}
