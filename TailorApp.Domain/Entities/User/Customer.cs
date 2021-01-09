using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TailorApp.Domain.Entities.Base;

namespace TailorApp.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(50)]
        public string Name { get; set; }

        
        [Required]
        [Column("Phone")]
        [Display(Name = "Phone No")]
        [StringLength(11)]
        public string Phone { get; set; }

        [Required]
        [Column("Address")]
        [Display(Name = "Address")]
        [StringLength(150)]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Register Date")]
        public DateTime RegisterDate { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile ImageUpload { get; set; }

        
       

    }
}
