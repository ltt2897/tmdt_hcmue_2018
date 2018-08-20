using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models;

namespace PhuKienDienThoai.ViewComponents
{
    ///<summary>class để xuất dữ liệu cho form thanh toán hóa đơn</summary>
    public class ThanhToanHoaDonViewComponent : ViewComponent
    {
        ApplicationDbContext context;
        UserManager<ApplicationUser> userManager;
        public ThanhToanHoaDonViewComponent(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager)
        {
            context = _context;
            userManager = _userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            return View(user);
        }
    }
}