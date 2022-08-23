using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Numito_Blog.DataLayer.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Slug { get; set; }
        [StringLength(400)]
        public string MetaTag { get; set; }
        [StringLength(500)]
        public string MetaDescription { get; set; }
        public int? ParentId { get; set; }

        #region Relations
        [InverseProperty("Category")]
        public ICollection<Post> Posts { get; set; }

        [InverseProperty("SubCategory")]
        public ICollection<Post> SubPosts { get; set; }
        #endregion
    }
}
