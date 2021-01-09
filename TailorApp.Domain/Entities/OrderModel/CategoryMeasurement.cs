using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.Base;

namespace TailorApp.Domain.Entities
{
    public class CategoryMeasurement : BaseEntity
    {
       
        public int CategoryID { get; set; }
        public int MeasurementID { get; set; }
      

        public Category Category { get; set; }
        public Measurement Measurement { get; set; }


    }
}
