using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Numito_Blog.Web.Areas.Admin.Models.Posts
{
    public class EditPostViewModel
    {
        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        public int CategoryId { get; set; }
        [Display(Name = "زیر دسته بندی")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        public int? SubCategoryId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        public string Title { get; set; }
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        public string Slug { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [UIHint("Ckeditor4")]
        public string Description { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile ImageFile { get; set; }
    }
}