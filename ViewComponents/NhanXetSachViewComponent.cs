using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.ViewComponents
{
    public class NhanXetSachViewComponent : ViewComponent
    {
        private ApplicationDbContext context;
        public NhanXetSachViewComponent(ApplicationDbContext dbcontext) => context = dbcontext;
        public Task<List<NhanXet>> GetList(int SachId)
        {
            return context.NhanXet
                        .Include(x => x.Sach)
                        .Include(x => x.User)
                        .Where(x => x.Sach.id == SachId)
                        .ToListAsync();
        }
        public async Task<IViewComponentResult> InvokeAsync(int SachId)
        {
            var data = await GetList(SachId);
            return View(data);
        }

    }
}