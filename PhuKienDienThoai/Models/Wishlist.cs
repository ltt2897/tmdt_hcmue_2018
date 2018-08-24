namespace PhuKienDienThoai.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore;

    public class Wishlist
    {
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int SanPhamID { get; set; }
        public virtual SanPham SanPham { get; set; }
        public Wishlist()
        {
            User = new ApplicationUser();
        }
    }
}