using System.Collections.Generic;
using Numito_Blog.CoreLayer.DTOs.Categories;
using Numito_Blog.CoreLayer.Utilities;

namespace Numito_Blog.CoreLayer.Services.Categories
{
    public interface ICategoryService
    {
        OperationResult CreateCategory(CreateCategoryDto command);
        OperationResult EditCategory(EditCategoryDto command);
        List<CategoryDto> GetAllCategory();
        List<CategoryDto> GetChildCategories(int parentId);
        CategoryDto GetCategory(int id);
        CategoryDto GetCategory(string slug);
        bool IsSlugExist(string slug);
    }
}