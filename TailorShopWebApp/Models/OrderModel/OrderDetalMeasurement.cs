using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;

namespace TailorManagementApp.Models
{
    public class OrderDetalMeasurement : BaseEntity
    {
        public int OrderDetailID { get; set; }
        public int MeasurementID { get; set; }

        public string MeasurementValue { get; set; }


        public OrderDetail OrderDetail { get; set; }
        public Measurement Measurement { get; set; }
    }
}
