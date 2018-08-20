using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuKienDienThoai.Data;

namespace PhuKienDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuanLyLienLacController : Controller
    {
        ILogger<QuanLyLienLacController> _logger;
        ApplicationDbContext context;
        public QuanLyLienLacController(ILogger<QuanLyLienLacController> logger, ApplicationDbContext _context)
        {
            _logger = logger;
            context = _context;
        }

        public IActionResult Index()
        {
            ViewData["HeadTitle"] = "Quản lý liên lạc";
            ViewData["TagName"] = "QuanLyLienLac";
            return View();
        }

        [AllowAnonymous]
        public async Task<List<Models.LienLacViewModels.ListLienLacViewModel>> GetList()
        {
            var model = await context.LienLac.Select(x => new Models.LienLacViewModels.ListLienLacViewModel()
            {
                Id = x.Id,
                TieuDe = x.TieuDe,
                NgayGoi = x.NgayGoi
            }).ToListAsync();
            return model;
        }

        // [HttpGet("[area]/[controller]/[action]/{id}")]
        public async Task<ActionResult> ChiTietLienLac(int id)
        {
            var model = await context.LienLac.FindAsync(id);
            
            return Ok(model);
        }
    }
}