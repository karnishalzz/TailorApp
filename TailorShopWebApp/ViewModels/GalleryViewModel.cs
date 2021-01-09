using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities;
using TailorApp.Domain.Entities.InventoryModel;

namespace TailorManagementApp.ViewModels
{
    public class GalleryViewModel
    {
        public IEnumerable<Stock> Stocks { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
