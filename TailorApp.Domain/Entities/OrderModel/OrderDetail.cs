using System.Collections.Generic;
using TailorApp.Domain.Entities.Base;

namespace TailorApp.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderDetailID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Paid { get; set; }
        //public bool IsDelivered { get; set; }


        public int OrderID { get; set; }
        public int CategoryID { get; set; }


        public virtual Category Category { get; set; }

        public virtual Order Order { get; set; }

        public ICollection<OrderDetailMeasurement> OrderDetailMeasurements { get; set; }
    }
}
