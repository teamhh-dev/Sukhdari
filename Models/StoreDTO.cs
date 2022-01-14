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

        [Required(ErrorMessage ="Please Enter Store Name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Enter Your Store Type")]
        public string Type { get; set; }
        [Required(ErrorMessage ="Enter Country Name")]
        public string Country { get; set; }
        
        
    }
}
