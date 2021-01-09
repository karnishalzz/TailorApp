using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.Base;
using TailorApp.Domain.Entities.InventoryModel;

namespace TailorApp.Domain.Entities.SalesModule
{
    public class SalesDetail : BaseEntity
    {
        [Key]
        public int SalesDetailID { get; set; }
        public int StockID { get; set; }
        public int SalesID { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }

        public  Stock Stock { get; set; }
        public  Sales Sales { get; set; }
    
    }
}
