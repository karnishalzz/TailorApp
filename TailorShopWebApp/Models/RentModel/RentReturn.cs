using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;

namespace TailorManagementApp.Models.RentModel
{
    public class RentReturn : BaseEntity
    {
        [Key]
        public int RentReturnID { get; set; }
        public int RentID { get; set; }
        public decimal Subtotal { get; set; }
        [Display(Name = "Discount(TK)")]
        public decimal Discount { get; set; }
        public decimal NetTotal { get; set; }
        public string Description { get; set; }
        [Display(Name = "Date (MM/DD/YYYY)")]
        public DateTime ReturnedDate { get; set; }

        public virtual ICollection<RentReturnDetail> RentReturnDetails { get; set; }
        public virtual Rent Rent { get; set; }
    }
    public class RentReturnDetail : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public int RentReturnID { get; set; }
        public int StockID { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }

        public virtual RentReturn RentReturn { get; set; }
    }
}

