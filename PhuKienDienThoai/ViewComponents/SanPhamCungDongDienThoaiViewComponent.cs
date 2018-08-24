using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models;

namespace PhuKienDienThoai.ViewComponents
{

    public class SanPhamCungDongDienThoaiViewComponent : ViewComponent
    {
        private ApplicationDbContext context;
        public SanPhamCungDongDienThoaiViewComponent(ApplicationDbContext _context) => context = _context;
        public async Task<IViewComponentResult> InvokeAsync(int DongDienThoaiId, int sanphamId)
        {
            var lst = await context.SanPham.Where(x => x.DongDienThoai.Id == DongDienThoaiId && x.id != sanphamId).ToListAsync();
            return View("Components/SanPhamChung/Default.cshtml", lst);
        }
    }
}