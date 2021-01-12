using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailorApp.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<SelectList> GetSelectListAsync(int? selectedCategoryId);
    }
}
