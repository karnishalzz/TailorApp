using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.PurchaseModel;

namespace TailorManagementApp.ViewModels
{
    public class PurchaseEntryViewModel
    {
        public DateTime Date { get; set; }
        public int SupplierID { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsPaid { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }

        public List<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
