using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using QuanLyBanSach.Areas.Admin.Models.SachViewModels;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Areas.Admin.Controllers
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
            var model =  context.Sach
                                    .Include(x => x.TacGia)
                                    .Include(x => x.NhaXuatBan);
                                   
            return View(model);
        }
        public async Task<IActionResult> SuaSach(int? id)
        {
            if (!id.HasValue || await context.Sach.FindAsync(id) == null)//tìm thông tin sách theo id)
            {//nếu không có id thì trở lại trang trước đó
                return NotFound();
            }
            var sach = await context.Sach.FindAsync(id);
            if(sach==null) //nếu mã số sai thì không có trang
                return NotFound();
            await context.Entry(sach).Reference(x => x.ChuDe).LoadAsync(); //load chủ đề vào sách
            await context.Entry(sach).Reference(x => x.TacGia).LoadAsync();
            await context.Entry(sach).Reference(x => x.DanhMuc).LoadAsync();
            await context.Entry(sach).Reference(x => x.NhaXuatBan).LoadAsync();
            var model = new SuaSachViewModel
            {
                Id = sach.id,
                ChieuDai = sach.ChieuDai,
                ChieuRong = sach.ChieuRong,
                ChuDeId = sach.ChuDeId,
                TacGiaId = sach.TacGiaId,
                DanhMucId = sach.DanhMucId,
                NhaXuatBanId = sach.NhaXuatBanId,
                DinhDang = sach.DinhDang,
                DonGia = sach.DonGia,
                TomTat = sach.TomTat,
                TenSach = sach.TenSach,
                SoLuong = sach.SoLuong,
                SoTrang = sach.SoTrang,
                HinhAnh = sach.HinhAnh,
                PhanTramGiamGia = sach.PhanTramGiamGia,
                ChuDes = await context.ChuDe.ToListAsync(),
                DanhMucs = await context.DanhMuc.ToListAsync(),
                NhaXuatBans = await context.NhaXuatBan.ToListAsync(),
                TacGias = await context.TacGia.ToListAsync(),
            };
            return View(model);
        }
        private async Task<string> uploadHinhAnh(int sachid, IFormFile ff)
        {
            var filename = sachid + "_" + ff.FileName;
            using (var fstream = new FileStream(environment.WebRootPath + "/images/Sach/" + filename, FileMode.Create))
            {
                await ff.CopyToAsync(fstream);
                fstream.Close();
                return filename;
            }
        }
        [HttpPost]
        public async Task<IActionResult> SuaSach(int id, SuaSachViewModel model)
        {
            var sach = await context.Sach.FindAsync(id);
            sach.ChieuDai = model.ChieuDai;
            sach.ChieuRong = model.ChieuRong;
            sach.ChuDeId = model.ChuDeId;
            sach.DanhMucId = model.DanhMucId;
            sach.DinhDang = model.DinhDang;
            sach.DonGia = model.DonGia;
            sach.NhaXuatBanId = model.NhaXuatBanId;
            sach.TomTat = model.TomTat;
            sach.TacGiaId = model.TacGiaId;
            sach.SoLuong = model.SoLuong;
            sach.SoTrang = model.SoTrang;
            sach.PhanTramGiamGia = model.PhanTramGiamGia;
            sach.TenSach = model.TenSach;

            //nếu muốn upload hình ảnh mới thì
            if (model.uploadHinhAnh != null)
            {
                //lấy file cũ
                var fileInfo = new FileInfo(environment.WebRootPath + "/images/Sach/" + sach.HinhAnh);
                if (fileInfo.Exists)//nếu trước đó có ảnh thì xóa ảnh đó đi
                    fileInfo.Delete();
                //upload ảnh mới
                sach.HinhAnh = await uploadHinhAnh(id, model.uploadHinhAnh);
            }
            context.Sach.Update(sach);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ThemSach()
        {
            var model = new ThemSachViewModel(context);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ThemSach(ThemSachViewModel model)
        {
            //nếu các model ko hợp lệ thì trả lại view báo lỗi validation
            if (!ModelState.IsValid)
            {
                model.ChuDes = await context.ChuDe.ToListAsync();
                model.DanhMucs = await context.DanhMuc.ToListAsync();
                model.NhaXuatBans = await context.NhaXuatBan.ToListAsync();
                model.TacGias = await context.TacGia.ToListAsync();
                return View(model);
            }
            var sach = new Sach
            {
                TenSach = model.TenSach,
                ChieuRong = model.ChieuRong,
                ChieuDai = model.ChieuDai,
                SoTrang = model.SoTrang,
                DinhDang = model.DinhDang,
                DonGia = model.DonGia,
                PhanTramGiamGia = model.PhanTramGiamGia,
                ChuDeId = model.ChuDeId,
                TacGiaId = model.TacGiaId,
                DanhMucId = model.DanhMucId,
                NhaXuatBanId = model.NhaXuatBanId,
                TomTat = model.TomTat,
            };
            await context.Sach.AddAsync(sach);
            await context.SaveChangesAsync();
            sach.HinhAnh = await uploadHinhAnh(sach.id, model.uploadHinhAnh);
            context.Sach.Update(sach);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> XoaSach(int id)
        {
            var sach = await context.Sach.FindAsync(id);
            var filepath = environment.WebRootPath + "/images/Sach/" + sach.HinhAnh;
            System.IO.File.Delete(filepath);
            context.Sach.Remove(sach);
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