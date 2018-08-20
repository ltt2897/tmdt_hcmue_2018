using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuKienDienThoai.Data;

namespace PhuKienDienThoai.ViewComponents
{
    public class ThuongHieuViewComponent : ViewComponent
    {
        ApplicationDbContext dbContext;
        public ThuongHieuViewComponent(ApplicationDbContext context) =>
            dbContext = context;
        private Task<List<Models.ThuongHieu>> getData() =>
            dbContext.ThuongHieu.ToListAsync();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await getData();
            return View(data);
        }
    }
}