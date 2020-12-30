using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;

namespace TailorManagementApp.Models
{
    public class Order : BaseEntity
    {
        public int OrderID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Order Placement Date (YYYY-MM-DD)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliverDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Delivery Date (YYYY-MM-DD)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Paid { get; set; }

        public bool IsDelivered { get; set; }
        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }
       
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
