﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models.SanPhamViewModels;
using PhuKienDienThoai.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using PhuKienDienThoai.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace PhuKienDienThoai.Controllers
{
    public class PaymentController : Controller
    {
        private IPaypalServices _PaypalServices;

        ApplicationDbContext context;
        UserManager<ApplicationUser> usermanager;
        IHostingEnvironment environment;

        public PaymentController(IPaypalServices paypalServices, ApplicationDbContext _c, UserManager<ApplicationUser> _usermanager, IHostingEnvironment _env)
        {
            _PaypalServices = paypalServices;
            context = _c;
            usermanager = _usermanager;
            environment = _env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePayment()
        {
            var stringItem = HttpContext.Session.GetString("GioHang");
            var ListItemTrongGioHang = new List<GioHangViewModel>();
            ListItemTrongGioHang = JsonConvert.DeserializeObject<List<GioHangViewModel>>(stringItem);
            string returnURL = Request.Scheme + "://" + Request.Host + "/Payment/ExecutePayment";
            string cancelURL = Request.Scheme + "://" + Request.Host + "/Payment/Cancel";
            var payment = _PaypalServices.CreatePayment(ListItemTrongGioHang, returnURL, cancelURL, "sale");

            return new JsonResult(payment);
        }

        public IActionResult ExecutePayment(string paymentId, string PayerID)
        {
            var payment = _PaypalServices.ExecutePayment(paymentId, PayerID);
            
            // Hint: You can save the transaction details to your database using payment/buyer info
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Success(string DiaChi, string GhiChu)
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
                            SanPhamId = item.SanPham.id,
                            SoLuong = item.SoLuong,
                            ThanhTien = item.SanPham.DonGia * item.SoLuong,
                        });
                    }

                    var HoaDon = await context.HoaDon.AddAsync(new Models.HoaDon
                    {
                        ChiTietHoaDons = ChiTietHoaDon,
                        DiaChi = WebUtility.UrlDecode(DiaChi),
                        GhiChu = WebUtility.UrlDecode(GhiChu),
                        PhuongThucThanhToan = "Đã thanh toán qua PayPal",
                        TongThanhTien = ChiTietHoaDon.Sum(x => x.ThanhTien),
                        User = currentUser,
                    });
                    await context.SaveChangesAsync();
                    
                    TempData["Message"] = "Đặt hàng thành công! Cảm ơn quý khách đã tin tưởng chúng tôi";
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

    }
}
