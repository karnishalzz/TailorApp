using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;

namespace TailorApp.Web.ViewModels
{
    public class InvoiceOrder
    {
   
        public int OrderDetailID { get; set; }
        public int Quantity { get; set; }
        public decimal TPrice { get; set; }
        public decimal TTotalPrice { get; set; }
        public decimal TPaid { get; set; }
   
        public string Category { get; set; }

    }
}
