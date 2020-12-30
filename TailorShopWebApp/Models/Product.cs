using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TailorManagementApp.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public Nullable<int> CategoryID { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        [Display(Name = "Product Image")]
        public IFormFile ImageUpload { get; set; }

        public Category Category { get; set; }
    }
}
