using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter Product Quantity")]
        public int Quantity { get; set; }
        public string Image { get; set; }

    }
}
