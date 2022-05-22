using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
    {
    public class Tag
        {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tag Name Required!")]
        public string Name { get; set; }
        [ForeignKey("StoreId")]
        public int StoreId { get; set; }
        }
    }