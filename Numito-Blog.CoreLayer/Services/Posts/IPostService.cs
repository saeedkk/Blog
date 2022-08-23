using Numito_Blog.CoreLayer.DTOs.Posts;
using Numito_Blog.CoreLayer.Utilities;

namespace Numito_Blog.CoreLayer.Services.Posts
{
    public interface IPostService
    {
        OperationResult CreatePost(CreatePostDto command);
        OperationResult EditPost(EditPostDto command);
        PostDto GetPostById(int postId);
        PostFilterDto GetPostByFilter(PostFilterParams filterParams);
        bool IsSlugExist(string slug);
    }
}