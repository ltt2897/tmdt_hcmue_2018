using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models;

namespace PhuKienDienThoai.ViewComponents
{
    public class NhanXetSanPhamViewComponent : ViewComponent
    {
        private ApplicationDbContext context;
        public NhanXetSanPhamViewComponent(ApplicationDbContext dbcontext) => context = dbcontext;
        public Task<List<NhanXet>> GetList(int SanPhamId)
        {
            return context.NhanXet
                        .Include(x => x.SanPham)
                        .Include(x => x.User)
                        .Where(x => x.SanPham.id == SanPhamId)
                        .ToListAsync();
        }
        public async Task<IViewComponentResult> InvokeAsync(int SanPhamId)
        {
            var data = await GetList(SanPhamId);
            return View(data);
        }

    }
}