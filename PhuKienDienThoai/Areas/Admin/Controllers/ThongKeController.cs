using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuKienDienThoai.Areas.Admin.Models.ThongKeViewModels;
using PhuKienDienThoai.Data;

namespace PhuKienDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ThongKeController : Controller
    {
        #region required fields
        ApplicationDbContext context;
        #endregion

        #region Contructor
        public ThongKeController(ApplicationDbContext _context) => context = _context;
        #endregion

        #region Index
        public IActionResult Index() {
            ViewData["TagName"] = "ThongKe";
            return View(new ThongKeModel());
        }
        #endregion
        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();
            base.Dispose(true);
        }
        #endregion

        #region API

        #region Doanh thu trong năm
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable> ThongKeDoanhThu(int Nam)
        {
            var data = new List<int>();
            for (int thang = 1; thang <= 12; thang++)
            {
                var dulieu = await context.HoaDon.Where(x => x.NgayLapHoaDon.Year == Nam && x.NgayLapHoaDon.Month == thang)
                                                .SumAsync(x => x.TongThanhTien);
                data.Add(dulieu);
            }
            return data;
        }
        #endregion

        #region Thống kê sản phẩm
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable ThongKeSanPhamBanDuoc(int thang, int nam)
        {
            var data = (
                from cthd in context.ChiTietHoaDon
                join hd in context.HoaDon on cthd.HoaDonId equals hd.Id
                join s in context.SanPham on cthd.SanPhamId equals s.id
                where cthd.HoaDon.NgayLapHoaDon.Month == thang && cthd.HoaDon.NgayLapHoaDon.Year == nam
                group cthd by s.TenSanPham into tensanpham
                select new
                {
                    name = tensanpham.Key,
                    y = tensanpham.Sum(x => x.SoLuong)
                }
            );
            return data;
        }
        #endregion

        #endregion
    }


}