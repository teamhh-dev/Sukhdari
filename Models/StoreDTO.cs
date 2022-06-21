using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StoreDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Store Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Your Store Type")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Enter Country Name")]
        public string Country { get; set; }
        public float? maxDiscount { get; set; }
        [Required]
        public string AdminName { get; set; }
        public string Image { get; set; }
        public int ClickCount { get; set; }

        public string Address { get; set; }
        [MaxLength(12)]
        public string phoneNo { get; set; }
        public string description { get; set; }
        public virtual ICollection<StoreImageDTO> StoreImages { get; set; }
        public List<string> ImageUrls { get; set; }

    }
}