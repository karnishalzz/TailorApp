using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models;

namespace TailorManagementApp.ViewModels
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Measurement> Measurements { get; set; }
        public IEnumerable<CategoryMeasurement> Enrollments { get; set; }
    }
}
