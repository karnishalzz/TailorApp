using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;

namespace TailorManagementApp.Models
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

        public ICollection<OrderDetalMeasurement> OrderDetalMeasurements { get; set; }
    }
}
