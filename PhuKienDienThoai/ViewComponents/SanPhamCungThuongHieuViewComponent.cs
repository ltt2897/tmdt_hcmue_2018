using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models;

namespace PhuKienDienThoai.ViewComponents
{

    public class SanPhamCungThuongHieuViewComponent : ViewComponent
    {
        private ApplicationDbContext context;
        public SanPhamCungThuongHieuViewComponent(ApplicationDbContext _context) => context = _context;
        public async Task<IViewComponentResult> InvokeAsync(int sanphamId, int ThuongHieuId)
        {
            var lst = await context.SanPham.Where(x => x.ThuongHieu.Id == ThuongHieuId && x.id != sanphamId).ToListAsync();
            return View("Components/SanPhamChung/Default.cshtml", lst);
        }
    }
}