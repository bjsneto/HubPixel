using Bogus;
using HubPixel.UnitTests.Common;
using DomainEntity = HubPixel.Domain.Entity;

namespace HubPixel.UnitTests.Domain.Entity.Category;
public class CategoryBuilder : BaseFixture
{
    private string _name;
    private string _description;
    private bool _isActive;
    
    public CategoryBuilder()
    {
        _name = GetValidCategoryName();
        _description = GetValidCategoryDescription();
        _isActive = true;
    }

    public CategoryBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CategoryBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public CategoryBuilder WithIsActive(bool isActive)
    {
        _isActive = isActive;
        return this;
    }

    public CategoryBuilder WithInvalidName()
    {
        _name = "";
        return this;
    }

    public CategoryBuilder WithNameGreaterThan255Characters()
    {
        _name = Faker.Lorem.Letter(256); 
        return this;
    }

    public DomainEntity.Category Build()
    {
        return new DomainEntity.Category(_name, _description, _isActive);
    }

    private string GetValidCategoryName()
    {
        var categoryName = string.Empty;
        while (categoryName!.Length < 3)
            categoryName = Faker.Commerce.Categories(1).FirstOrDefault();
        if (categoryName.Length > 255)
            categoryName = categoryName[..255];
        return categoryName;
    }

    private string GetValidCategoryDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();
        if (categoryDescription.Length > 10_000)
            categoryDescription = categoryDescription[..10_000];
        return categoryDescription;
    }
}