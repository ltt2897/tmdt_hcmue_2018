<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
=======
﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
>>>>>>> Duc_2
using PaypalExpressCheckout.BusinessLogic.Interfaces;


namespace QuanLyBanSach.Controllers
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
<<<<<<< HEAD
=======
            // WebClient client = new WebClient();
            // client.Headers.Add("referer", "http://stackoverflow.com");
            // client.Headers.Add("user-agent", "Mozilla/5.0");
>>>>>>> Duc_2
            string returnURL = Request.Scheme + "://" + Request.Host + "/Payment/ExecutePayment";
            string cancelURL = Request.Scheme + "://" + Request.Host + "/Payment/Cancel";
            var payment = _PaypalServices.CreatePayment(100, returnURL, cancelURL, "sale");

            return new JsonResult(payment);
        }

        public IActionResult ExecutePayment(string paymentId, string token, string PayerID)
        {
            var payment = _PaypalServices.ExecutePayment(paymentId, PayerID);

            // Hint: You can save the transaction details to your database using payment/buyer info
<<<<<<< HEAD

=======
>>>>>>> Duc_2
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
