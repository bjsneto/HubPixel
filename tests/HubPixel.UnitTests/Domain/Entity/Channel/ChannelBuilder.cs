using Bogus;
using HubPixel.UnitTests.Common;
using DomainEntity = HubPixel.Domain.Entity;

namespace HubPixel.UnitTests.Domain.Entity.Channel;
public class ChannelBuilder : BaseFixture
{
    private string _name;
    private string _description;
    private string _urlStream;
    private readonly List<Guid> _categoryIds = [];

    public ChannelBuilder()
    {
        _name = GetValidChannelName();
        _description = GetValidChannelDescription();
        _urlStream = GetValidUrlStream();
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

    public ChannelBuilder WithCategoryIds(IEnumerable<Guid> categoryIds)
    {
        _categoryIds.AddRange(categoryIds);
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
        return DomainEntity.Channel.Create(_name, _description, _urlStream, _categoryIds);
    }

    private string GetValidChannelName() => Faker.Company.CatchPhrase();
    private string GetValidChannelDescription() => Faker.Lorem.Paragraph(5);
    private string GetValidUrlStream() => Faker.Internet.Url();
}
