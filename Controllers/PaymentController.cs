﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models.SanPhamViewModels;
using PhuKienDienThoai.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using PaypalExpressCheckout.BusinessLogic.Interfaces;
using System.Collections.Generic;

namespace PhuKienDienThoai.Controllers
{
    public class PaymentController : Controller
    {
        private IPaypalServices _PaypalServices;

        public PaymentController(IPaypalServices paypalServices)
        {
            _PaypalServices = paypalServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePayment()
        {
            //string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPayPal?";
            // WebClient client = new WebClient();
            // client.Headers.Add("referer", "http://stackoverflow.com");
            // client.Headers.Add("user-agent", "Mozilla/5.0");
            //lấy dữ liệu từ session
            var stringItem = HttpContext.Session.GetString("GioHang");
            var ListItemTrongGioHang = new List<GioHangViewModel>();
            ListItemTrongGioHang = JsonConvert.DeserializeObject<List<GioHangViewModel>>(stringItem);
            string returnURL = Request.Scheme + "://" + Request.Host + "/Payment/ExecutePayment";
            string cancelURL = Request.Scheme + "://" + Request.Host + "/Payment/Cancel";
            var payment = _PaypalServices.CreatePayment(ListItemTrongGioHang.Count, returnURL, cancelURL, "sale", ListItemTrongGioHang);

            return new JsonResult(payment);
        }

        public IActionResult ExecutePayment(string paymentId, string PayerID)
        {
            var payment = _PaypalServices.ExecutePayment(paymentId, PayerID);

            // Hint: You can save the transaction details to your database using payment/buyer info
            return Ok();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }

    }
}
