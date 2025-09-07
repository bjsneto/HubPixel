namespace HubPixel.Application.MediaSource;
public interface IMediaSourceService
{
    public Task CreateAsync(CreateMediaSourceInput input, CancellationToken cancellationToken);
}
