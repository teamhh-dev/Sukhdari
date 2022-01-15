﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public int StoreId { get; set; }
        [Required(ErrorMessage = "Category Name Required!")]
        public string Name { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store store { get; set; }

    }
}