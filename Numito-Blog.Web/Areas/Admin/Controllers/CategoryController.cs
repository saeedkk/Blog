using Microsoft.AspNetCore.Mvc;
using Numito_Blog.CoreLayer.DTOs.Categories;
using Numito_Blog.CoreLayer.Services.Categories;
using Numito_Blog.CoreLayer.Utilities;
using Numito_Blog.Web.Areas.Admin.Models.Categories;

namespace Numito_Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategory());
        }
        [Route("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId)
        {
            return View();
        }
        [HttpPost("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId,CreateCategoryViewModel createvViewModel)
        {
            createvViewModel.ParentId = parentId;
            var result = _categoryService.CreateCategory(createvViewModel.MapToDto());

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(createvViewModel.Slug), result.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategory(id);
            if (category == null)
                return RedirectToAction("Index");

            var model = new EditCategoryViewModel()
            {
                Title = category.Title,
                Slug = category.Slug,
                MetaTag = category.MetaTag,
                MetaDescription = category.MetaDescription
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditCategoryViewModel editModel)
        {
            var result = _categoryService.EditCategory(new EditCategoryDto()
            {
                Id = id,
                Title = editModel.Title,
                Slug = editModel.Slug,
                MetaTag = editModel.MetaTag,
                MetaDescription = editModel.MetaDescription
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(editModel.Slug),result.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult GetChildCategories(int parentId)
        {
            var category = _categoryService.GetChildCategories(parentId);

            return new JsonResult(category);
        }
    }
}
