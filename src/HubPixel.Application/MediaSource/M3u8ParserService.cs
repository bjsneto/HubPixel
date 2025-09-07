using HubPixel.Domain.Entity;
using System.Text.RegularExpressions;

namespace HubPixel.Application.MediaSource;
public class M3u8ParserService : IM3u8ParserService
{
    public (List<Channel> channels, List<Category> categories) ParseFile(string m3u8Content)
    {
        var channels = new List<Channel>();
        var categories = new List<Category>();
        var categoryNames = new HashSet<string>();

        var lines = m3u8Content.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("#EXTINF:"))
            {
                var groupMatch = Regex.Match(lines[i], @"group-title=""([^""]+)""");
                var nameMatch = Regex.Match(lines[i], @"tvg-name=""([^""]+)""");

                if (!groupMatch.Success || !nameMatch.Success)
                {
                    continue;
                }

                string categoryName = groupMatch.Groups[1].Value;
                string channelName = nameMatch.Groups[1].Value;

                if (i + 1 < lines.Length && !lines[i + 1].StartsWith("#"))
                {
                    var urlStream = lines[i + 1].Trim();

                    var channel = Channel.Create(channelName, channelName, urlStream, new List<Guid>());
                    channels.Add(channel);

                    if (!categoryNames.Contains(categoryName))
                    {
                        var category = Category.Create(categoryName, categoryName);
                        categories.Add(category);
                        categoryNames.Add(categoryName);
                        channel.AddCategories(new List<Guid> { category.Id });
                    }
                    else
                    {
                        var existingCategory = categories.First(c => c.Name == categoryName);
                        channel.AddCategories(new List<Guid> { existingCategory.Id });
                    }
                }
            }
        }
        return (channels, categories);
    }
}