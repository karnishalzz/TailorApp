using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TailorApp.Domain.Entities.Base;

namespace TailorApp.Domain.Entities
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
