using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TailorManagementApp.Models.Base;
using TailorManagementApp.Models.PurchaseModel;

namespace TailorManagementApp.Models.InventoryModel
{
    public class Stock : BaseEntity
    {
       
        public int StockID { get; set; }
        [Required]
        public int ItemID { get; set; }

        [Required]
        [Display(Name = "Categeory")]
        [EnumDataType(typeof(CategoryType))]
        public CategoryType Category { get; set; }


        [Range(0, 9999999)]
        public int InitialQuantity { get; set; }

        [Required]
        [Range(0, 100000)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Out of range!")]
        public decimal CostPrice { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Out of range!")]
        public decimal SellingPrice { get; set; }

        public int PurchaseID { get; set; }


        //references
        public virtual Item Item { get; set; }
        public virtual Purchase Purchase { get; set; }


    }
    public enum CategoryType
    {
        Rent,
        Sale,
        Others
    }
}
