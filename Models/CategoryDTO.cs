using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name Required!")]
        public string Name { get; set; }
        public string Description { get; set; }
        public float? DiscountPercentage { get; set; }
        public int StoreId { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
