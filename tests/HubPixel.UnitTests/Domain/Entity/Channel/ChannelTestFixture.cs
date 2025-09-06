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

    public ChannelBuilder GetChannelBuilder() => new ChannelBuilder();

    public DomainEntity.Channel GetValidChannel() => GetChannelBuilder().Build();

    public DomainEntity.Channel GetValidChannelWithCategories(int categoryCount = 3)
    {
        var categoryIds = Enumerable.Range(0, categoryCount).Select(_ => Guid.NewGuid()).ToList();

        return GetChannelBuilder()
            .WithCategoryIds(categoryIds)
            .Build();
    }
}
[CollectionDefinition(nameof(ChannelTestFixture))]
public class ChannelTestFixtureCollection
    : ICollectionFixture<ChannelTestFixture>
{
}