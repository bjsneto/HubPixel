using Bogus;
using HubPixel.UnitTests.Common;
using DomainEntity = HubPixel.Domain.Entity;

namespace HubPixel.UnitTests.Domain.Entity.Channel;
public class ChannelBuilder : BaseFixture
{
    private string _name;
    private string _description;
    private string _urlStream;
    private List<DomainEntity.Category> _categories;

    public ChannelBuilder()
    {
        _name = GetValidChannelName();
        _description = GetValidChannelDescription();
        _urlStream = GetValidUrlStream();
        _categories = [];
    }

    public ChannelBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ChannelBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public ChannelBuilder WithUrlStream(string urlStream)
    {
        _urlStream = urlStream;
        return this;
    }

    public ChannelBuilder WithCategories(List<DomainEntity.Category> categories)
    {
        _categories = categories;
        return this;
    }

    public ChannelBuilder AddCategory(DomainEntity.Category category)
    {
        _categories.Add(category);
        return this;
    }

    public ChannelBuilder WithInvalidName()
    {
        _name = "";
        return this;
    }

    public ChannelBuilder WithInvalidUrlStream()
    {
        _urlStream = "";
        return this;
    }

    public DomainEntity.Channel Build()
    {
        var channel = new DomainEntity.Channel(_name, _description, _urlStream);

        if (_categories.Any())
        {
            channel.AddCategories(_categories);
        }
        return channel;
    }

    private string GetValidChannelName()
    {
        var channelName = string.Empty;
        while (channelName.Length < 3 || channelName.Length > 255)
            channelName = Faker.Company.CatchPhrase();
        return channelName;
    }

    private string GetValidChannelDescription()
    {
        var channelDescription = Faker.Lorem.Paragraph(5);
        if (channelDescription.Length > 10_000)
            channelDescription = channelDescription[..10_000];
        return channelDescription;
    }

    private string GetValidUrlStream()
    {
        return Faker.Internet.Url();
    }
}
