using Microsoft.AspNetCore.Mvc;
using Numito_Blog.CoreLayer.DTOs.Posts;
using Numito_Blog.CoreLayer.Services.Posts;
using Numito_Blog.CoreLayer.Utilities;
using Numito_Blog.Web.Areas.Admin.Models.Posts;

namespace Numito_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index(int pageId=1, string title="", string categorySlug="")
        {
            var model = _postService.GetPostByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                Title = title,
                PageId = pageId,
                Take = 20
            });
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreatePostViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = _postService.CreatePost(new CreatePostDto()
            {
                CategoryId = createViewModel.CategoryId,
                Title = createViewModel.Title,
                Description = createViewModel.Description,
                ImageFile = createViewModel.ImageFile,
                Slug = createViewModel.Slug,
                SubCategoryId = createViewModel.SubCategoryId == 0 ? null : createViewModel.SubCategoryId,
                UserId = User.GetUserId()
            });

            if (result != OperationResult.Success())
                return View();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var post = _postService.GetPostById(id);
            if (post == null)
                return RedirectToAction("Index");

            var model = new EditPostViewModel()
            {
                CategoryId = post.CategoryId,
                Description = post.Description,
                Slug = post.Slug,
                SubCategoryId = post.SubCategoryId,
                Title = post.Title
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditPostViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = _postService.EditPost(new EditPostDto()
            {
                CategoryId = editViewModel.CategoryId,
                Title = editViewModel.Title,
                Description = editViewModel.Description,
                ImageFile = editViewModel.ImageFile,
                Slug = editViewModel.Slug,
                SubCategoryId = editViewModel.SubCategoryId == 0 ? null : editViewModel.SubCategoryId,
                PostId = id
            });

            if (result != OperationResult.Success())
                return View();

            return RedirectToAction("Index");
        }
    }
}
