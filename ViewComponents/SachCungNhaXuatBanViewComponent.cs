using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.ViewComponents
{

    public class SachCungNhaXuatBanViewComponent : ViewComponent
    {
        private ApplicationDbContext context;
        public SachCungNhaXuatBanViewComponent(ApplicationDbContext _context) => context = _context;
        public async Task<IViewComponentResult> InvokeAsync(int sachId, int NhaXuatBanId)
        {
            var lst = await context.Sach.Where(x => x.NhaXuatBan.Id == NhaXuatBanId && x.id != sachId).ToListAsync();
            return View("Components/SachChung/Default.cshtml", lst);
        }
    }
}