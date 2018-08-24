using PayPal.Api;
using System.Collections.Generic;
using PhuKienDienThoai.Models.SanPhamViewModels;

namespace PhuKienDienThoai.Models
{
    public interface IPaypalServices
    {
        Payment CreatePayment(List<GioHangViewModel> list, string returnUrl, string cancelUrl, string intent);

        Payment ExecutePayment(string paymentId, string payerId);
    }
}