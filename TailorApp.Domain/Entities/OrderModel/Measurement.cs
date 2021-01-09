using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.Base;

namespace TailorApp.Domain.Entities
{
    public class Measurement : BaseEntity
    {
        [Key]
        public int MeasurementID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Measurement")]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public ICollection<CategoryMeasurement> Enrollments { get; set; }
        
        public ICollection<OrderDetalMeasurement> OrderDetalMeasurements { get; set; }
    }
}
