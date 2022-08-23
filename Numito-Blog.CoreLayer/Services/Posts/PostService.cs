using System.Linq;
using Microsoft.EntityFrameworkCore;
using Numito_Blog.CoreLayer.DTOs.Posts;
using Numito_Blog.CoreLayer.Mappers;
using Numito_Blog.CoreLayer.Services.FileManager;
using Numito_Blog.CoreLayer.Utilities;
using Numito_Blog.DataLayer.Context;

namespace Numito_Blog.CoreLayer.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly BlogContext _context;
        private readonly IFileManager _fileManager;
        public PostService(BlogContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public OperationResult CreatePost(CreatePostDto command)
        {
            if (command.ImageFile == null)
                return OperationResult.Error();
            var post = PostMapper.MapCreateDtoToPost(command);

            if (IsSlugExist(post.Slug))
                return OperationResult.Error("شناسه تکراری است");

            post.ImageName = _fileManager.SaveFileAndReturnName(command.ImageFile, Directories.PostImage);
            _context.Posts.Add(post);
            _context.SaveChanges();

            return OperationResult.Success();
        }

        public OperationResult EditPost(EditPostDto command)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == command.PostId);
            if (post == null)
                return OperationResult.NotFound();

            PostMapper.EditPost(command, post);

            if (command.ImageFile != null)
                post.ImageName = _fileManager.SaveFileAndReturnName(command.ImageFile, Directories.PostImage);

            _context.SaveChanges();
            return OperationResult.Success();
        }

        public PostDto GetPostById(int postId)
        {
            var post = _context.Posts
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .FirstOrDefault(c => c.Id == postId);
            return PostMapper.MapToDto(post);
        }

        public PostFilterDto GetPostByFilter(PostFilterParams filterParams)
        {
            var result = _context.Posts
                .Include(d => d.Category)
                .Include(d => d.SubCategory)
                .OrderByDescending(d => d.CreatedDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.CategorySlug))
                result = result.Where(r => r.Slug == filterParams.CategorySlug);

            if (!string.IsNullOrWhiteSpace(filterParams.Title))
                result = result.Where(r => r.Title.Contains(filterParams.Title));

            var skip = (filterParams.PageId - 1) * filterParams.Take;
            var pageCount = result.Count() / filterParams.Take;

            return new PostFilterDto()
            {
                Posts = result.Skip(skip).Take(filterParams.Take).Select(post => PostMapper.MapToDto(post)).ToList(),
                FilterParams = filterParams,
                PageCount = pageCount
            };
        }

        public bool IsSlugExist(string slug)
        {
            return _context.Posts.Any(p => p.Slug == slug.ToSlug());
        }
    }
}