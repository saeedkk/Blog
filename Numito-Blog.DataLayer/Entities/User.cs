using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Numito_Blog.DataLayer.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public UserRole Role { get; set; }

        #region Relations

        public ICollection<Post> Posts { get; set; }
        public ICollection<PostComment> PostComments { get; set; }

        #endregion
    }

    public enum UserRole
    {
        Admin,
        User,
        Author
    }
}
