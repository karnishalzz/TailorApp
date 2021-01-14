using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.RentModel;
using TailorApp.Domain.Entities.SalesModule;

namespace TailorApp.Web.ViewModels
{
    public class IncomeViewModel
    {
        public Income Income { get; set; }
        public Sales Sales { get; set; }
        public Order Order { get; set; }

        public Rent Rent { get; set; }
    }
}
