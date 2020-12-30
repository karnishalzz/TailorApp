using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;
using TailorManagementApp.Models.PurchaseModel;

namespace TailorManagementApp.Models
{
    public class Expense : BaseEntity
    {
        [Key]
        public int ExpenseID { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Out of range!")]
        public decimal Price{ get; set; }

        [Required]
        [StringLength(100)]
      
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date (YYYY-MM-DD)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public ExpenseType Type { get; set; }
        public int? PurchaseID { get; set; }

        public virtual Purchase Purchase { get; set; }
    }
    public enum ExpenseType
    {
        Purchase,
        Indoor,
        Utilities,
        [Display(Name = "Employee Wages")]
        EmployeeWages,
        [Display(Name = "Office Supplies")]
        OfficeSupplies,
        [Display(Name = "Shop Rent")]
        ShopRent,
        Others
    }
}
