using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;

namespace TailorManagementApp.Models
{
    public class Income : BaseEntity
    {
        [Key]
        public int IncomeID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Type")]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Out of range!")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date (YYYY-MM-DD)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public int? SalesID { get; set; }
        public int? OrderID { get; set; }
        public int? RentID { get; set; }
    }
}
