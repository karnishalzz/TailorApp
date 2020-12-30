using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;
using TailorManagementApp.Models.InventoryModel;

namespace TailorManagementApp.Models.RentModel
{
    public class RentDetail : BaseEntity
    {
        [Key]
        public int RentDetailID { get; set; }
        public int StockID { get; set; }
        public int RentID { get; set; }
        [Required]
        public int Quantity { get; set; }

        public int ReturnQuantity { get; set; }
        [Required]
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }

        public Stock Stock { get; set; }
        public Rent Rent { get; set; }
    }
}
