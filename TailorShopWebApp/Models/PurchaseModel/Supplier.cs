using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;

namespace TailorManagementApp.Models.PurchaseModel
{
    public class Supplier : BaseEntity
    {
        public Supplier()
        {
            this.Address = "N/A";
        }
        public int SupplierID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Only 50 characters allowed!")]
        public string Name { get; set; }

        public string Address { get; set; }
        [Required]
        public string Contact { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
