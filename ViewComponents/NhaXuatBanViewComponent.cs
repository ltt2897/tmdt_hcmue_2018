using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Data;

namespace QuanLyBanSach.ViewComponents
{
    public class NhaXuatBanViewComponent : ViewComponent
    {
        ApplicationDbContext dbContext;
        public NhaXuatBanViewComponent(ApplicationDbContext context) =>
            dbContext = context;
        private Task<List<Models.NhaXuatBan>> getData() =>
            dbContext.NhaXuatBan.ToListAsync();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await getData();
            return View(data);
        }
    }
}