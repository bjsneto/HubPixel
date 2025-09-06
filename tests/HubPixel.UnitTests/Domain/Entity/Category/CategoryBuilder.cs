using Bogus;
using HubPixel.UnitTests.Common;
using DomainEntity = HubPixel.Domain.Entity;

namespace HubPixel.UnitTests.Domain.Entity.Category;
public class CategoryBuilder : BaseFixture
{
    private string _name;
    private string _description;
   
    public CategoryBuilder()
    {
        _name = GetValidCategoryName();
        _description = GetValidCategoryDescription(); 
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

    public DomainEntity.Category Build()
    {
        return DomainEntity.Category.Create(_name, _description);
    }

    public DomainEntity.Category Build(bool throwException)
    {
        if (throwException)
        {
            return DomainEntity.Category.Create(_name, _description);
        }
        else
        {
            return DomainEntity.Category.Create(_name, _description);
        }
    }

    public CategoryBuilder WithInvalidName()
    {
        _name = "";
        return this;
    }

    public CategoryBuilder WithNameGreaterThan100Characters() // Ajustado para 100 caracteres
    {
        _name = Faker.Lorem.Letter(101);
        return this;
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