﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }
        public int CategoryID { get; set; }
        // [ForeignKey("CategoryID")]
        // public virtual Category Category { get; set; }


    }
}