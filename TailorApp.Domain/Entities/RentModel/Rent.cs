using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.Base;

namespace TailorApp.Domain.Entities.RentModel
{
    public class Rent : BaseEntity
    {
        [Key]
        public int RentID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Rent Date")]
        public DateTime RentDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal GrandTotal { get; set; }
        [Display(Name = "Advanced")]
        public decimal AdvancePayment { get; set; }
        public decimal Paid { get; set; }
        public string Remarks { get; set; }
        public int CustomerID { get; set; }

        public bool IsPaid { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<RentDetail> RentDetails { get; set; }
        public virtual ICollection<RentReturn> RentReturns { get; set; }
    }
}
