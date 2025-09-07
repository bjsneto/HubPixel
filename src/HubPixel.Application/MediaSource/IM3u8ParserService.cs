using HubPixel.Domain.Entity;

namespace HubPixel.Application.MediaSource;
public interface IM3u8ParserService
{
    public (List<Channel> channels, List<Category> categories) ParseFile(string m3u8Content);
}
