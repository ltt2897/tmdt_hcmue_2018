using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models.SachViewModels;
using Microsoft.AspNetCore.Identity;
using QuanLyBanSach.Models;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using QuanLyBanSach.Services;
using QuanLyBanSach.Repositories.Email;

namespace QuanLyBanSach.Controllers
{

    public class GioHangController : Controller
    {
        ApplicationDbContext context;
        UserManager<ApplicationUser> usermanager;
        IHostingEnvironment environment;

        public GioHangController(ApplicationDbContext _c, UserManager<ApplicationUser> _usermanager, IHostingEnvironment _env)
        {
            context = _c;
            usermanager = _usermanager;
            environment = _env;
        }
        //action để xử lý khi người dùng chưa đăng nhập mà lại muốn thêm thông tin vào giỏ hàng 
        public IActionResult ThemVaoGioHang(string returnurl = null)
        {
            if (returnurl != null)
                return Redirect(returnurl);
            return RedirectToAction(
                actionName: "Index",
                controllerName: "Home"
            );

        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> ThemVaoGioHang(int SachId, int SoLuong)
        {
            try
            {
                var stringItem = HttpContext.Session.GetString("GioHang");
                //nếu trước đó chưa lưu vào giỏ hàng một món hàng nào
                var sach = await context.Sach.FindAsync(SachId);
                if (sach.SoLuong == 0)
                {
                    TempData["message"] = "Sản phẩm đã hết, vui lòng chọn mua sản phẩm khác";
                    return Redirect(Request.Headers["Referer"].ToString());
                }
                var lst = new List<GioHangViewModel>();
                if (!string.IsNullOrEmpty(stringItem))//nếu đã có danh sách rồi
                {
                    /*
                        nếu đây không phải là lần mua đầu tiền thì lấy danh sách ra từ session
                        do danh sách lấy ra là kiểu JSON.stringify nên phải parse lại kiểu list
                    */
                    lst = JsonConvert.DeserializeObject<List<GioHangViewModel>>(stringItem);
                }
                var kiemTraItemDaTonTaiTrongGioHang = lst.Find(x => x.Sach.id == SachId);

                if (kiemTraItemDaTonTaiTrongGioHang == null)
                    //thêm món hàng vào danh sách
                    lst.Add(new GioHangViewModel
                    {
                        Sach = sach,
                        SoLuong = SoLuong
                    });
                else
                {
                    lst.Find(x => x.Sach.id == SachId).SoLuong += SoLuong;
                }
                //sau khi thêm vào giỏ hàng thì trừ số lượng tồn của sách trong csdl
                sach.SoLuong -= SoLuong;
                //cập nhật lại sách
                context.Sach.Update(sach);
                await context.SaveChangesAsync();
                //thêm vào session giỏ hàng với giá trị là chuỗi json  
                HttpContext.Session.SetString("GioHang", JsonConvert.SerializeObject(lst));
                return Ok();
            }
            catch (System.Exception ex)
            {
                Logs.Logger.Log(ex.Message);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public IActionResult SuaGioHang(int SachId, int SoLuong)
        {
            try
            {
                //lấy hết các sản phẩm trong session
                var getStringGioHang = HttpContext.Session.GetString("GioHang");
                //chuyển qua json
                var ListItemTrongGioHang = JsonConvert.DeserializeObject<List<GioHangViewModel>>(getStringGioHang);
                //cập nhật lại danh sách
                ListItemTrongGioHang.Find(sach => sach.Sach.id == SachId).SoLuong = SoLuong;
                //chuyển lại chuỗi
                getStringGioHang = JsonConvert.SerializeObject(ListItemTrongGioHang);
                //gắn vào lại json
                HttpContext.Session.SetString("GioHang", getStringGioHang);
                return Ok("Cập nhật thành công");
            }
            catch (System.Exception ex)
            {
                Logs.Logger.Log(ex.ToString());
                return BadRequest("Có lỗi trong quá trinh thực hiện");
            }
        }
        public async Task<IActionResult> XoaGioHang(int SachId)
        {
            //lấy giá trị trong session giỏ hàng ra dưới dạng chuỗi
            var stringList = HttpContext.Session.GetString("GioHang");
            if (!string.IsNullOrEmpty(stringList))
            {   //nếu nó không rỗng thì chuyển thành kiểu list
                var ListItemTrongGioHang = JsonConvert.DeserializeObject<List<GioHangViewModel>>(stringList);
                //lấy ra item cần xóa
                var itemCanXoa = ListItemTrongGioHang.Find(item => item.Sach.id == SachId);
                //xóa item vừa lấy ra
                ListItemTrongGioHang.Remove(itemCanXoa);
                //cập nhật lại số lượng tồn trong csdl 
                var sach = await context.Sach.FindAsync(SachId);
                //cộng lại số lượng đã trừ ra
                sach.SoLuong += itemCanXoa.SoLuong;
                //cập nhật csdl
                context.Sach.Update(sach);
                await context.SaveChangesAsync();
                //gắn lại vào giỏ hàng
                HttpContext.Session.SetString("GioHang", JsonConvert.SerializeObject(ListItemTrongGioHang));
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var getStringGioHang = HttpContext.Session.GetString("GioHang");
            var data = new List<GioHangViewModel>();
            if (!string.IsNullOrEmpty(getStringGioHang))
                data = JsonConvert.DeserializeObject<List<GioHangViewModel>>(getStringGioHang);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ThanhToanHoaDon(string DiaChi, string GhiChu)
        {
            //lấy dữ liệu từ session
            var stringItem = HttpContext.Session.GetString("GioHang");
            var currentUser = await usermanager.GetUserAsync(User);
            var ListItemTrongGioHang = new List<GioHangViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(stringItem))
                {
                    ListItemTrongGioHang = JsonConvert.DeserializeObject<List<GioHangViewModel>>(stringItem);
                    var ChiTietHoaDon = new List<Models.ChiTietHoaDon>();

                    foreach (var item in ListItemTrongGioHang)
                    {
                        ChiTietHoaDon.Add(new Models.ChiTietHoaDon
                        {
                            SachId = item.Sach.id,
                            SoLuong = item.SoLuong,
                            ThanhTien = item.Sach.DonGia * item.SoLuong,
                        });
                    }

                    var HoaDon = await context.HoaDon.AddAsync(new Models.HoaDon
                    {
                        ChiTietHoaDons = ChiTietHoaDon,
                        DiaChi = DiaChi,
                        GhiChu = GhiChu,
                        TongThanhTien = ChiTietHoaDon.Sum(x => x.ThanhTien),
                        User = currentUser,
                    });
                    await context.SaveChangesAsync();

                    TempData["Message"] = "Đặt hàng thành công! Cảm ơn quý khách đã tin tưởng chúng tôi";
                    //template path
                    var template = new EmailTemplate(environment);
                    var emailReceipttemplatePath = template.ReceiptTemplatePath("EmailReceipt");
                    var NoiDungReceiptTemplatePath = template.ReceiptTemplatePath("EmailReceiptContent");
                    
                    var NoiDungEmailTemplate = "";
                    using (var NoiDungfileStream = System.IO.File.OpenText(NoiDungReceiptTemplatePath))
                    {
                        var NoiDungTemplate = await NoiDungfileStream.ReadToEndAsync();
                        foreach (var item in ListItemTrongGioHang)
                        {
                            NoiDungEmailTemplate += NoiDungTemplate.Replace("{TenSanPham}", item.Sach.TenSach)
                                                        .Replace("{SoLuong}", item.SoLuong.ToString())
                                                        .Replace("{ThanhTien}", item.ThanhTien.ToString("#,0"))
                                                        .Replace("{SanPhamUrl}", Url.Action(nameof(SachController.Index), nameof(SachController), new { id = item.Sach.id }));
                            NoiDungfileStream.Close();
                        }
                    }
                    var ResceiptTemplate = "";
                    using (var EmailReceiptfileStream = System.IO.File.OpenText(emailReceipttemplatePath))
                    {
                        ResceiptTemplate = await EmailReceiptfileStream.ReadToEndAsync();
                        ResceiptTemplate = ResceiptTemplate.Replace("{DiaChi}", DiaChi)
                                                            .Replace("{TongThanhTien}", HoaDon.Entity.TongThanhTien.ToString())
                                                            .Replace("{NoiDung}", NoiDungEmailTemplate);
                        EmailReceiptfileStream.Close();
                    };
                    var _email = new EmailSender();
                    await _email.SendEmailAsync(currentUser.Email, "Xác nhận thanh toán", ResceiptTemplate);
                    HttpContext.Session.Clear();
                }
            }
            catch (System.Exception ex)
            {
                Logs.Logger.Log(ex.ToString());
            }
            return RedirectToAction(
                actionName: "Index",
                controllerName: "Home"
            );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                usermanager.Dispose();
                context.Dispose();
            }
            base.Dispose(true);
        }
    }
}