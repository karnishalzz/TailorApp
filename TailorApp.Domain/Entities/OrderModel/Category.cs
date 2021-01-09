using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.Base;

namespace TailorApp.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public ICollection<CategoryMeasurement> Enrollments { get; set; }
        public ICollection<Product> Products { get; set; }



    }
}
