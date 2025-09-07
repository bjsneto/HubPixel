using HubPixel.Domain.Entity;
using HubPixel.Domain.Repository.MediaSource;

namespace HubPixel.Infrastructure.Data.Repositories;

public class MediaSourceRepository : IMediaSourceRepository
{
    private readonly HubPixelDbContext _context;

    public MediaSourceRepository(HubPixelDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MediaSource mediaSource, CancellationToken cancellationToken)
    {
        await _context.MediaSources.AddAsync(mediaSource, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
