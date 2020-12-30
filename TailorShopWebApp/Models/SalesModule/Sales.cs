using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;

namespace TailorManagementApp.Models.SalesModule
{
    public class Sales : BaseEntity
    {
        [Key]
        public int SalesID { get; set; }
        [Display(Name = "Sale Date (MM/DD/YYYY)")]
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        public string Remarks { get; set; }

        public virtual ICollection<SalesDetail> SalesItems { get; set; }
    

    }
}
