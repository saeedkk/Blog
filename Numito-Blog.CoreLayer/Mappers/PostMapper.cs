using Numito_Blog.CoreLayer.DTOs.Posts;
using Numito_Blog.CoreLayer.Utilities;
using Numito_Blog.DataLayer.Entities;

namespace Numito_Blog.CoreLayer.Mappers
{
    public class PostMapper
    {
        public static Post MapCreateDtoToPost(CreatePostDto dto)
        {
            return new Post()
            {
                Title = dto.Title,
                Description = dto.Description,
                Slug = dto.Slug.ToSlug(),
                CategoryId = dto.CategoryId,
                UserId = dto.UserId,
                Visit = 0,
                IsDelete = false,
                SubCategoryId = dto.SubCategoryId
            };
        }

        public static PostDto MapToDto(Post post)
        {
            return new PostDto()
            {
                Title = post.Title,
                Description = post.Description,
                Slug = post.Slug.ToSlug(),
                CategoryId = post.CategoryId,
                UserId = post.UserId,
                Visit = post.Visit,
                CreatedDate = post.CreatedDate,
                Category = CategoryMapper.Map(post.Category),
                ImageName = post.ImageName,
                PostId = post.Id,
                SubCategoryId = post.SubCategoryId,
                SubCategory = post.SubCategoryId == null ? null : CategoryMapper.Map(post.SubCategory)
            };
        }

        public static Post EditPost(EditPostDto editDto, Post post)
        {
            post.Title = editDto.Title;
            post.Description = editDto.Description;
            post.Slug = editDto.Slug.ToSlug();
            post.CategoryId = editDto.CategoryId;
            post.SubCategoryId = editDto.SubCategoryId;
            return post;
        }
    }
}