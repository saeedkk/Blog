using Numito_Blog.CoreLayer.DTOs.Categories;
using Numito_Blog.DataLayer.Entities;

namespace Numito_Blog.CoreLayer.Mappers
{
    public class CategoryMapper
    {
        public static CategoryDto Map(Category category)
        {
            return new CategoryDto()
            {
                MetaDescription = category.MetaDescription,
                MetaTag = category.MetaTag,
                Slug = category.Slug,
                Title = category.Title,
                ParentId = category.ParentId,
                Id = category.Id
            };
        }
    }
}