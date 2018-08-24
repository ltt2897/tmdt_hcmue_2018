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

    }
}
