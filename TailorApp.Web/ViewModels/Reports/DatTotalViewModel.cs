using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TailorApp.Web.ViewModels.Reports
{
    public class DatTotalViewModel
    {
        public int Day { get; set; }
        public decimal Total { get; set; }
    }
    public class SalesViewModel
    {
        public DateTime Date { get; set; }
        public List<DatTotalViewModel> Days { get; set; }
    }
}
