using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.Base;

namespace TailorApp.Domain.Entities
{
    public class OrderDetailMeasurement : BaseEntity
    {
        public int OrderDetailID { get; set; }
        public int MeasurementID { get; set; }

        public string MeasurementValue { get; set; }


        public OrderDetail OrderDetail { get; set; }
        public Measurement Measurement { get; set; }
    }
}
