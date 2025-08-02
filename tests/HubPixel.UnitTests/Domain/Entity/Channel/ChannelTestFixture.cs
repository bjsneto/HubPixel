using HubPixel.UnitTests.Domain.Entity.Category;
using DomainEntity = HubPixel.Domain.Entity;

namespace HubPixel.UnitTests.Domain.Entity.Channel;
public class ChannelTestFixture
{
    private readonly CategoryTestFixture _categoryTestFixture;
    public ChannelTestFixture()
        : base()
    {
        _categoryTestFixture = new CategoryTestFixture();
    }

    public ChannelBuilder GetChannelBuilder()
    {
        return new ChannelBuilder();
    }

    public DomainEntity.Channel GetValidChannel()
    {
        return GetChannelBuilder().Build();
    }

    public List<DomainEntity.Channel> GetChannelList(int count = 10)
    {
        var channels = new List<DomainEntity.Channel>();
        for (int i = 0; i < count; i++)
        {
            channels.Add(GetChannelBuilder().Build());
        }
        return channels;
    }

    public DomainEntity.Channel GetValidChannelWithCategories(int categoryCount = 3)
    {
        var categories = _categoryTestFixture.GetCategoryList(categoryCount);
        return GetChannelBuilder()
            .WithCategories(categories)
            .Build();
    }

}

[CollectionDefinition(nameof(ChannelTestFixture))]
public class ChannelTestFixtureCollection
    : ICollectionFixture<ChannelTestFixture>
{
}