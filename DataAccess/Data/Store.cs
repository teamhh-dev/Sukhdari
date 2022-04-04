using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Store
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Type { get; set; }
        
        [Required]
        public string Country { get; set; }
        //[ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual ICollection<StoreImage> StoreImages { get; set; }
    }
}
