using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailorApp.Application.Services
{
    public interface ICategoryService
    {
        Task<SelectList> GetSelectListAsync(int? selectedCategoryId);
    }
}
