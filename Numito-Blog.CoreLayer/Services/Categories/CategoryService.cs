using System;
using System.Collections.Generic;
using System.Linq;
using Numito_Blog.CoreLayer.DTOs.Categories;
using Numito_Blog.CoreLayer.Mappers;
using Numito_Blog.CoreLayer.Utilities;
using Numito_Blog.DataLayer.Context;
using Numito_Blog.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Numito_Blog.CoreLayer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogContext _context;

        public CategoryService(BlogContext context)
        {
            _context = context;
        }

        public OperationResult CreateCategory(CreateCategoryDto command)
        {
            if (IsSlugExist(command.Slug))
                return OperationResult.Error("شناسه تکراری است.");

            var category = new Category()
            {
                Title = command.Title,
                IsDelete = false,
                ParentId = command.ParentId,
                Slug = command.Slug.ToSlug(),
                MetaTag = command.MetaTag,
                MetaDescription = command.MetaDescription
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditCategory(EditCategoryDto command)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == command.Id);
            if(category==null)
                return OperationResult.NotFound();

            if(command.Slug.ToSlug()!=category.Slug)
                if (IsSlugExist(command.Slug))
                    return OperationResult.Error("شناسه تکراری است.");

            category.Title = command.Title;
            category.MetaDescription = command.MetaDescription;
            category.MetaTag = command.MetaTag;
            category.Slug = command.Slug.ToSlug();

            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<CategoryDto> GetAllCategory()
        {
            return _context.Categories.Select(category=>CategoryMapper.Map(category)).ToList();
        }

        public List<CategoryDto> GetChildCategories(int parentId)
        {
            return _context.Categories.Where(r=>r.ParentId == parentId).Select(category => CategoryMapper.Map(category)).ToList();
        }

        public CategoryDto GetCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return null;

            return CategoryMapper.Map(category);
        }

        public CategoryDto GetCategory(string slug)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Slug == slug);

            if (category == null)
                return null;

            return CategoryMapper.Map(category);
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Categories.Any(c => c.Slug == slug.ToSlug());
        }
    }
}