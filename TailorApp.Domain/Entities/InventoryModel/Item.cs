using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using TailorApp.Domain.Entities.Base;
using TailorApp.Domain.Entities.PurchaseModel;

namespace TailorApp.Domain.Entities.InventoryModel
{
    public class Item : BaseEntity
    {
        public int ItemID { get; set; }

        [Required]
        [Display(Name = "Item")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Unit")]
        public UnitType? Unit { get; set; }

        public string Description { get; set; }

        [Display(Name = "Last Update(YYYY-MM-DD)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LastUpdated { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        [Display(Name = "Item Image")]
        public IFormFile ImageUpload { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetail { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }

    }


    public enum UnitType
    {
        pkg, file, pcs, ml, mg, gm, kg, other
    }
}

