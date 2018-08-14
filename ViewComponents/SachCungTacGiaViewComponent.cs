using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.ViewComponents
{

    public class SachCungTacGiaViewComponent : ViewComponent
    {
        private ApplicationDbContext context;
        public SachCungTacGiaViewComponent(ApplicationDbContext _context) => context = _context;
        public async Task<IViewComponentResult> InvokeAsync(int tacgiaId, int sachId)
        {
            var lst = await context.Sach.Where(x => x.TacGia.Id == tacgiaId && x.id != sachId).ToListAsync();
            return View("Components/SachChung/Default.cshtml", lst);
        }
    }
}