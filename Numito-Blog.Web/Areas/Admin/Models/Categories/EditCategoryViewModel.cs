using System.ComponentModel.DataAnnotations;

namespace Numito_Blog.Web.Areas.Admin.Models.Categories
{
    public class EditCategoryViewModel
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
    }
}