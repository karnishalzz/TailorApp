using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TailorManagementApp.ViewModels.Reports
{
    public class MonthTotalViewModel
    {
        public int Month { get; set; }
        public decimal Total { get; set; }
    }
    public class YearSalesViewModel
    {
        public DateTime Date { get; set; }
        public List<MonthTotalViewModel> Months { get; set; }
    }
}

