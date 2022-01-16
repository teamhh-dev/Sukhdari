using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name Required!")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int StoreId { get; set; }
    }
}
