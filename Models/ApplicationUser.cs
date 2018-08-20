using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PhuKienDienThoai.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public ICollection<Wishlist> Wishlist { get; set; }
        public ICollection<HoaDon> HoaDons { get; set; }
        public ICollection<NhanXet> NhanXets { get; set; }
        public ApplicationUser() : base()
        {
            Wishlist = new HashSet<Wishlist>();
            HoaDons = new HashSet<HoaDon>();
            NhanXets = new HashSet<NhanXet>();
        }
    }
}
