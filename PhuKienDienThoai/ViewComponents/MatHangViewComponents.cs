using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhuKienDienThoai.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PhuKienDienThoai.ViewComponents
{

    public class MatHangViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext dbContext;
        public MatHangViewComponent(ApplicationDbContext context) => dbContext = context;

        private Task<List<Models.MatHang>> GetList() =>
            dbContext.MatHang
                    .Include(x => x.SanPhames)
                    .ThenInclude(x => x.DanhMuc)
                    .ToListAsync();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await GetList();
            return View(data);
        }
    }
}