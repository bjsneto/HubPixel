using FluentAssertions;

namespace HubPixel.UnitTests.Domain.Entity.Channel;

[Collection(nameof(ChannelTestFixture))]
public class ChannelTest
{
    private readonly ChannelTestFixture _fixture;
    public ChannelTest(ChannelTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(InstantiateWithValidParameters))]
    [Trait("Domain", "Channel - Aggregate")]
    public void InstantiateWithValidParameters()
    {
        var channelBuilder = _fixture.GetChannelBuilder();
        var channel = channelBuilder.Build();

        channel.Should().NotBeNull();
        channel.Name.Should().Be(channel.Name);
        channel.Description.Should().Be(channel.Description);
        channel.UrlStream.Value.Should().Be(channel.UrlStream.Value);
        channel.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        channel.CategoryIds.Should().BeEmpty();
    }

    [Theory(DisplayName = nameof(ThrowExceptionWhenNameIsInvalid))]
    [Trait("Domain", "Channel - Aggregate")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ThrowExceptionWhenNameIsInvalid(string invalidName)
    {
        var channelBuilder = _fixture.GetChannelBuilder();
        Action act = () => channelBuilder.WithName(invalidName).Build();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Name cannot be null or empty.");
    }

    [Theory(DisplayName = nameof(ThrowExceptionWhenUrlIsInvalid))]
    [Trait("Domain", "Channel - Aggregate")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("invalid-url")]
    public void ThrowExceptionWhenUrlIsInvalid(string invalidUrl)
    {
        var channelBuilder = _fixture.GetChannelBuilder();
        Action act = () => channelBuilder.WithUrlStream(invalidUrl).Build();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Invalid URL stream format.");
    }

    [Fact(DisplayName = nameof(AddCategories))]
    [Trait("Domain", "Channel - Aggregate")]
    public void AddCategories()
    {
        var channel = _fixture.GetValidChannel();
        var categoryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

        channel.AddCategories(categoryIds);

        channel.CategoryIds.Should().HaveCount(3);
        channel.CategoryIds.Should().Contain(categoryIds);
    }

    [Fact(DisplayName = nameof(RemoveCategories))]
    [Trait("Domain", "Channel - Aggregate")]
    public void RemoveCategories()
    {
        var categoryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
        var channel = _fixture.GetChannelBuilder().WithCategoryIds(categoryIds).Build();

        var idsToRemove = new List<Guid> { categoryIds[0], categoryIds[2] };

        channel.RemoveCategories(idsToRemove);

        channel.CategoryIds.Should().HaveCount(1);
        channel.CategoryIds.Should().NotContain(idsToRemove);
        channel.CategoryIds.Should().Contain(categoryIds[1]);
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Channel - Aggregate")]
    public void Update()
    {
        var channel = _fixture.GetValidChannel();
        var newName = _fixture.GetChannelBuilder().Build().Name;
        var newDescription = _fixture.GetChannelBuilder().Build().Description;
        var newUrl = _fixture.GetChannelBuilder().Build().UrlStream.Value;

        channel.Update(newName, newDescription, newUrl);

        channel.Name.Should().Be(newName);
        channel.Description.Should().Be(newDescription);
        channel.UrlStream.Value.Should().Be(newUrl);
    }
}
