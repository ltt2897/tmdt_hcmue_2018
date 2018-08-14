namespace QuanLyBanSach.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore;

    public class Wishlist
    {
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int SachID { get; set; }
        public virtual Sach Sach { get; set; }
        public Wishlist()
        {
            User = new ApplicationUser();
        }
    }
}