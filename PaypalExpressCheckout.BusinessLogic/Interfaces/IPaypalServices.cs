using PayPal.Api;
using System.Collections.Generic;
using PhuKienDienThoai.Models.SanPhamViewModels;

namespace PaypalExpressCheckout.BusinessLogic.Interfaces
{
    public interface IPaypalServices
    {
        Payment CreatePayment(decimal amount, string returnUrl, string cancelUrl, string intent, List<GioHangViewModel> list);

        Payment ExecutePayment(string paymentId, string payerId);
    }
}