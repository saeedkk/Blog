using System.ComponentModel.DataAnnotations;
using Numito_Blog.CoreLayer.DTOs.Categories;

namespace Numito_Blog.Web.Areas.Admin.Models.Categories
{
    public class CreateCategoryViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        public string Title { get; set; }
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        public string Slug { get; set; }
        [Display(Name = "MetaTag (با - از هم جدا کنید)")]
        public string MetaTag { get; set; }
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }
        public int? ParentId { get; set; }

        public CreateCategoryDto MapToDto()
        {
            return new CreateCategoryDto()
            {
                Title = Title,
                Slug = Slug,
                MetaTag = MetaTag,
                MetaDescription = MetaDescription,
                ParentId = ParentId
            };
        }
    }
}