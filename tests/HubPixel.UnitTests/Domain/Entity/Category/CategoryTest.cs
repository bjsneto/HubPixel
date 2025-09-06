using FluentAssertions;
using DomainEntity = HubPixel.Domain.Entity;

namespace HubPixel.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{
    private readonly CategoryTestFixture _fixture;

    public CategoryTest(CategoryTestFixture fixture) => _fixture = fixture;

    [Fact(DisplayName = nameof(InstantiateWithValidParameters))]
    [Trait("Domain", "Category - Aggregate")]
    public void InstantiateWithValidParameters()
    {
        var categoryBuilder = _fixture.GetCategoryBuilder();
        var category = categoryBuilder.Build();

        category.Should().NotBeNull();
        category.Name.Should().Be(category.Name);
        category.Description.Should().Be(category.Description);
        category.IsActive.Should().BeTrue();
        category.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Theory(DisplayName = nameof(ThrowExceptionWhenNameIsInvalid))]
    [Trait("Domain", "Category - Aggregate")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ThrowExceptionWhenNameIsInvalid(string invalidName)
    {
        var categoryBuilder = _fixture.GetCategoryBuilder();
        Action action = () => categoryBuilder.WithName(invalidName).Build();

        action.Should().Throw<ArgumentException>()
            .WithMessage("Name cannot be null or empty.");
    }

    [Fact(DisplayName = nameof(ThrowExceptionWhenNameIsTooLong))]
    [Trait("Domain", "Category - Aggregate")]
    public void ThrowExceptionWhenNameIsTooLong()
    {
        var categoryBuilder = _fixture.GetCategoryBuilder();
        Action action = () => categoryBuilder.WithNameGreaterThan100Characters().Build();

        action.Should().Throw<ArgumentException>()
            .WithMessage("Name cannot exceed 100 characters.");
    }

    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Category - Aggregate")]
    public void Activate()
    {
        var category = _fixture.GetValidCategory();
        category.Deactivate(); // Deixa a categoria inativa para o teste

        category.Activate();

        category.IsActive.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(Deactivate))]
    [Trait("Domain", "Category - Aggregate")]
    public void Deactivate()
    {
        var category = _fixture.GetValidCategory();

        category.Deactivate();

        category.IsActive.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Category - Aggregate")]
    public void Update()
    {
        var category = _fixture.GetValidCategory();
        var newName = _fixture.GetCategoryBuilder().Build().Name;
        var newDescription = _fixture.GetCategoryBuilder().Build().Description;

        category.Update(newName, newDescription);

        category.Name.Should().Be(newName);
        category.Description.Should().Be(newDescription);
    }
}
