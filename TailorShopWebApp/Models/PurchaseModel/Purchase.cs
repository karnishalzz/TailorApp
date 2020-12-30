using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;

namespace TailorManagementApp.Models.PurchaseModel
{
    public class Purchase : BaseEntity
    {
        [Key]
        public int PurchaseID { get; set; }
        [Display(Name = "Date (YYYY-MM-DD)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        public decimal Amount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
        [Required]
        public decimal GrandTotal { get; set; }
        //public bool IsPaid { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LastUpdated { get; set; }
        public string Description { get; set; }

        
        public virtual Supplier Supplier { get; set; }
        
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
       
    }
}
