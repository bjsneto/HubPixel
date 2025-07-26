using HubPixel.UnitTests.Common;
using DomainEntity = HubPixel.Domain.Entity;
namespace HubPixel.UnitTests.Domain.Entity.Category;

public class CategoryTestFixture : BaseFixture
{
    public CategoryTestFixture()
        : base() { }

    public CategoryBuilder GetCategoryBuilder() => new();

    public DomainEntity.Category GetValidCategory()
    {
        return GetCategoryBuilder().Build();
    }

    public List<DomainEntity.Category> GetCategoryList(int count = 10)
    {
        var categories = new List<DomainEntity.Category>();
        for (int i = 0; i < count; i++)
        {
            categories.Add(GetCategoryBuilder().Build());
        }
        return categories;
    }
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection
  : ICollectionFixture<CategoryTestFixture>
{
}
