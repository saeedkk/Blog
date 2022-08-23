using System;
using System.ComponentModel.DataAnnotations;


namespace Numito_Blog.DataLayer.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
