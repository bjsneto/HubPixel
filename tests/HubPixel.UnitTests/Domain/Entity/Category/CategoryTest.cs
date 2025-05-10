using FluentAssertions;
using DomainEntity = HubPixel.Domain.Entity;

namespace HubPixel.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{
    private readonly CategoryTestFixture _fixture;

    public CategoryTest(CategoryTestFixture fixture) => _fixture = fixture;

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregate")]
    public void Instantiate()
    {
        var validCategory = _fixture.GetValidCategory();
        var isActive = _fixture.GetRandomBoolean();
        var datetimeBefore = DateTime.UtcNow;

        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, isActive);
        var datetimeAfter = DateTime.UtcNow.AddSeconds(1);

        category.Should().NotBeNull();
        category.Name.Should().Be(validCategory.Name);
        category.Description.Should().Be(validCategory.Description);
        category.IsActive.Should().Be(isActive);
        category.CreatedAt.Should().NotBeSameDateAs(unexpected: default);
        (category.CreatedAt >= datetimeBefore).Should().BeTrue();
        (category.CreatedAt <= datetimeAfter).Should().BeTrue();
    }

    [Fact(DisplayName = nameof(InstantiateWithEmptyName))]
    [Trait("Domain", "Category - Aggregate")]
    public void InstantiateWithEmptyName()
    {
        Action act = () => new DomainEntity.Category("", "Description");

        act.Should().Throw<ArgumentException>()
            .WithMessage("Name cannot be empty.");
    }

    [Fact(DisplayName = nameof(InstantiateWithDeactive))]
    [Trait("Domain", "Category - Aggregate")]
    public void InstantiateWithDeactive()
    {
        var category = new DomainEntity.Category("Category", "Description", false);
        category.Should().NotBeNull();
        category.Name.Should().Be("Category");
        category.Description.Should().Be("Description");
        category.IsActive.Should().BeFalse();
        category.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}
