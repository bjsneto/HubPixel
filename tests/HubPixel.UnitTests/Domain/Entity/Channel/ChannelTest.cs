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

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Channel - Aggregate")]
    public void Instantiate()
    {
        var datetimeBefore = DateTime.UtcNow;
        var channel = _fixture.GetChannelBuilder().Build();

        channel.Should().NotBeNull();
        channel.Name.Should().Be(channel.Name);
        channel.Description.Should().Be(channel.Description);
        channel.UrlStream.Should().Be(channel.UrlStream);
        channel.CreatedAt.Should().NotBeSameDateAs(unexpected: default);
        (channel.CreatedAt >= datetimeBefore).Should().BeTrue();
        (channel.CreatedAt <= DateTime.UtcNow.AddSeconds(1)).Should().BeTrue();
        channel.Categories.Should().BeEmpty();
    }

    [Fact(DisplayName = nameof(AddCategoryToChannel))]
    [Trait("Domain", "Channel - Aggregate")]
    public void AddCategoryToChannel()
    {
        var channel = _fixture.GetValidChannelWithCategories(10);
        channel.Categories.Should().HaveCount(10);
    }

    [Fact(DisplayName = nameof(InstantiateWithEmptyName))]
    [Trait("Domain", "Channel - Aggregate")]
    public void InstantiateWithEmptyName()
    {
        Action act = () => _fixture.GetChannelBuilder().WithInvalidName().Build();

        act.Should().Throw<ArgumentException>()
            .WithMessage("Name cannot be empty.");
    }

    [Fact(DisplayName = nameof(InstantiateWithEmptyUrlStream))]
    [Trait("Domain", "Channel - Aggregate")]
    public void InstantiateWithEmptyUrlStream()
    {
        Action act = () => _fixture.GetChannelBuilder().WithInvalidUrlStream().Build();

        act.Should().Throw<ArgumentException>()
            .WithMessage("UrlStream cannot be empty.");
    }

    [Fact(DisplayName = nameof(InstantiateWithProvidedData))]
    [Trait("Domain", "Channel - Aggregate")]
    public void InstantiateWithProvidedData()
    {
        var expectedChannel = _fixture.GetValidChannel();
        var expectedName = expectedChannel.Name;
        var expectedDescription = expectedChannel.Description;
        var expectedUrl = expectedChannel.UrlStream;

        var channel = _fixture.GetChannelBuilder()
            .WithName(expectedName)
            .WithDescription(expectedDescription)
            .WithUrlStream(expectedUrl)
            .Build();

        channel.Should().NotBeNull();
        channel.Name.Should().Be(expectedName);
        channel.Description.Should().Be(expectedDescription);
        channel.UrlStream.Should().Be(expectedUrl);
        channel.Categories.Should().BeEmpty();
    }

}
