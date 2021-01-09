using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.Base;
using TailorApp.Domain.Entities.InventoryModel;

namespace TailorApp.Domain.Entities.RentModel
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
