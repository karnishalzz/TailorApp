using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models;
using TailorManagementApp.Models.RentModel;
using TailorManagementApp.Models.SalesModule;

namespace TailorManagementApp.ViewModels
{
    public class IncomeViewModel
    {
        public Income Income { get; set; }
        public Sales Sales { get; set; }
        public Order Order { get; set; }

        public Rent Rent { get; set; }
    }
}
