using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TailorManagementApp.Models.Base
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
