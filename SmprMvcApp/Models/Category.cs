using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmprMvcApp.Models
{
    public class Category
    {
        //[Key]=> eğer Id olarak tanımlaycak olursak buna gerek yok
        public int Id { get; set; }//Id => Primary key, otomotik artan tam sayı olarak tanımladık.

        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Enter a value between 1 and 100")] //server side validations
        public int DisplayOrder { get; set; }
    }
}