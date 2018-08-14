using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanSach.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanSach.ViewComponents
{

    public class ChuDeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext dbContext;
        public ChuDeViewComponent(ApplicationDbContext context) => dbContext = context;

        private Task<List<Models.ChuDe>> GetList() =>
            dbContext.ChuDe
                    .Include(x => x.Saches)
                    .ThenInclude(x => x.DanhMuc)
                    .ToListAsync();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await GetList();
            return View(data);
        }
    }
}