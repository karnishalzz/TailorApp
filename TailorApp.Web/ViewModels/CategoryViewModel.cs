using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;

namespace TailorApp.Web.ViewModels
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Measurement> Measurements { get; set; }
        public IEnumerable<CategoryMeasurement> Enrollments { get; set; }
    }
}
