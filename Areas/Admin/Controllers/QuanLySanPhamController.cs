using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PhuKienDienThoai.Areas.Admin.Models.SanPhamViewModels;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models;

namespace PhuKienDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuanLySanPhamController : Controller
    {
        ApplicationDbContext context;
        IHostingEnvironment environment;
        public QuanLySanPhamController(ApplicationDbContext _context, IHostingEnvironment env)
        {
            context = _context;
            environment = env;
        }

        public IActionResult Index()
        {
            ViewData["TagName"] = "QuanLySanPham";
            var model = context.SanPham
                            .Include(x => x.DongDienThoai)
                            .Include(x => x.ThuongHieu);
                                   
            return View(model);
        }
        public async Task<IActionResult> SuaSanPham(int? id)
        {
            if (!id.HasValue || await context.SanPham.FindAsync(id) == null)//tìm thông tin sản phẩm theo id)
            {//nếu không có id thì trở lại trang trước đó
                return NotFound();
            }
            var sanpham = await context.SanPham.FindAsync(id);
            if(sanpham==null) //nếu mã số sai thì không có trang
                return NotFound();
            await context.Entry(sanpham).Reference(x => x.MatHang).LoadAsync(); //load mặt hàng vào sản phẩm
            await context.Entry(sanpham).Reference(x => x.DongDienThoai).LoadAsync();
            await context.Entry(sanpham).Reference(x => x.DanhMuc).LoadAsync();
            await context.Entry(sanpham).Reference(x => x.ThuongHieu).LoadAsync();
            var model = new SuaSanPhamViewModel
            {
                Id = sanpham.id,
                MatHangId = sanpham.MatHangId,
                DongDienThoaiId = sanpham.DongDienThoaiId,
                DanhMucId = sanpham.DanhMucId,
                ThuongHieuId = sanpham.ThuongHieuId,
                DinhDang = sanpham.DinhDang,
                DonGia = sanpham.DonGia,
                TomTat = sanpham.TomTat,
                TenSanPham = sanpham.TenSanPham,
                SoLuong = sanpham.SoLuong,
                MauSac = sanpham.MauSac,
                HinhAnh = sanpham.HinhAnh,
                PhanTramGiamGia = sanpham.PhanTramGiamGia,
                MatHangs = await context.MatHang.ToListAsync(),
                DanhMucs = await context.DanhMuc.ToListAsync(),
                ThuongHieus = await context.ThuongHieu.ToListAsync(),
                DongDienThoais = await context.DongDienThoai.ToListAsync(),
            };
            return View(model);
        }
        private async Task<string> uploadHinhAnh(int sanphamid, IFormFile ff)
        {
            var filename = sanphamid + "_" + ff.FileName;
            using (var fstream = new FileStream(environment.WebRootPath + "/images/SanPham/" + filename, FileMode.Create))
            {
                await ff.CopyToAsync(fstream);
                fstream.Close();
                return filename;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SuaSanPham(int id, SuaSanPhamViewModel model)
        {
            var sanpham = await context.SanPham.FindAsync(id);
            sanpham.MatHangId = model.MatHangId;
            sanpham.DanhMucId = model.DanhMucId;
            sanpham.DinhDang = model.DinhDang;
            sanpham.DonGia = model.DonGia;
            sanpham.ThuongHieuId = model.ThuongHieuId;
            sanpham.TomTat = model.TomTat;
            sanpham.DongDienThoaiId = model.DongDienThoaiId;
            sanpham.SoLuong = model.SoLuong;
            sanpham.MauSac = model.MauSac;
            sanpham.PhanTramGiamGia = model.PhanTramGiamGia;
            sanpham.TenSanPham = model.TenSanPham;

            //nếu muốn upload hình ảnh mới thì
            if (model.uploadHinhAnh != null)
            {
                //lấy file cũ
                var fileInfo = new FileInfo(environment.WebRootPath + "/images/SanPham/" + sanpham.HinhAnh);
                if (fileInfo.Exists)//nếu trước đó có ảnh thì xóa ảnh đó đi
                    fileInfo.Delete();
                //upload ảnh mới
                sanpham.HinhAnh = await uploadHinhAnh(id, model.uploadHinhAnh);
            }
            context.SanPham.Update(sanpham);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ThemSanPham()
        {
            var model = new ThemSanPhamViewModel
            {
                ThuongHieuId = context.ThuongHieu.Min(x => x.Id),
                MatHangId = context.MatHang.Min(x => x.Id),
                DanhMucId = context.DanhMuc.Min(x => x.Id),
                DongDienThoaiId = context.DongDienThoai.Min(x => x.Id),
                MatHangs = context.MatHang.ToList(),
                DanhMucs = context.DanhMuc.ToList(),
                ThuongHieus = context.ThuongHieu.ToList(),
                DongDienThoais = context.DongDienThoai.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ThemSanPham(ThemSanPhamViewModel model)
        {
            //nếu các model ko hợp lệ thì trả lại view báo lỗi validation
            if (!ModelState.IsValid)
            {
                model.MatHangs = await context.MatHang.ToListAsync();
                model.DanhMucs = await context.DanhMuc.ToListAsync();
                model.ThuongHieus = await context.ThuongHieu.ToListAsync();
                model.DongDienThoais = await context.DongDienThoai.ToListAsync();
                return View(model);
            }
            var sanpham = new SanPham
            {
                TenSanPham = model.TenSanPham,
                MauSac = model.MauSac,
                DinhDang = model.DinhDang,
                DonGia = model.DonGia,
                PhanTramGiamGia = model.PhanTramGiamGia,
                MatHangId = model.MatHangId,
                DongDienThoaiId = model.DongDienThoaiId,
                DanhMucId = model.DanhMucId,
                ThuongHieuId = model.ThuongHieuId,
                TomTat = model.TomTat,
            };
            await context.SanPham.AddAsync(sanpham);
            await context.SaveChangesAsync();
            sanpham.HinhAnh = await uploadHinhAnh(sanpham.id, model.uploadHinhAnh);
            context.SanPham.Update(sanpham);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> XoaSanPham(int id)
        {
            var sanpham = await context.SanPham.FindAsync(id);
            var filepath = environment.WebRootPath + "/images/SanPham/" + sanpham.HinhAnh;
            System.IO.File.Delete(filepath);
            context.SanPham.Remove(sanpham);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();

        }
    }
}