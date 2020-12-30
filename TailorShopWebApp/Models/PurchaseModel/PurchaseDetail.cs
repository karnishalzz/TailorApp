﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;
using TailorManagementApp.Models.InventoryModel;

namespace TailorManagementApp.Models.PurchaseModel
{
    public class PurchaseDetail : BaseEntity
    {
        [Key]
        public int PurchaseDetailID { get; set; }
        public int PurchaseID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string Category { get; set; }



        public virtual Item Item { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
