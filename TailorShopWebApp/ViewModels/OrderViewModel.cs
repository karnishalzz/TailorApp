using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TailorManagementApp.ViewModels
{
    public class OrderViewModel
    {
        public DateTime Date { get; set; }
        public int CustomerID { get; set; }
        public List<ListItems> Items { get; set; }
        public List<List<MeasurementList>> ListOfMeasurement { get; set; }

    }

    public class ListItems
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int CategoryID { get; set; }
        public decimal Paid { get; set; }
       

    }
    public class MeasurementList
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    }
