using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Staj.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Diplayer Order")]
        [Range(1, 100, ErrorMessage = "Display Order Must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
