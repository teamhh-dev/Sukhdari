using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Store
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Store Name required!")]
        public string StoreName { get; set; }

        [Required(ErrorMessage = "Country Name required!")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Store Type required!")]
        public string StoreType { get; set; }

        public string User { get; set; }


    }
}
