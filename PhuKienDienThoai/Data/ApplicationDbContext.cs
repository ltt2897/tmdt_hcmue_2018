using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhuKienDienThoai.Models;

namespace PhuKienDienThoai.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<DongDienThoai> DongDienThoai { get; set; }
        public DbSet<ThuongHieu> ThuongHieu { get; set; }
        public DbSet<NhanXet> NhanXet { get; set; }
        public DbSet<DanhMuc> DanhMuc { get; set; }
        public DbSet<MatHang> MatHang { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<LienLac> LienLac { get; set; }
        public DbSet<TraLoiLienLac> TraLoiLienLac { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
			builder.Entity<SanPham>().HasKey(p => p.id);
			builder.Entity<ChiTietHoaDon>()
					.HasKey(p => new { p.HoaDonId, p.SanPhamId });

			builder.Entity<ChiTietHoaDon>()
					.HasOne(p => p.SanPham)
					.WithMany(p => p.ChiTietHoaDons)
					.HasForeignKey(p => p.SanPhamId);

			builder.Entity<ChiTietHoaDon>()
					.HasOne(p => p.HoaDon)
					.WithMany(p => p.ChiTietHoaDons)
					.HasForeignKey(p => p.HoaDonId);

			builder.Entity<Wishlist>()
					.HasKey(p => new { p.SanPhamID, p.UserID });

			builder.Entity<Wishlist>()
					.HasOne(p => p.SanPham)
					.WithMany(p => p.Wishlist)
					.HasForeignKey(p => p.SanPhamID);

			builder.Entity<Wishlist>()
					.HasOne(p => p.User)
					.WithMany(p => p.Wishlist)
					.HasForeignKey(p => p.UserID);

			builder.Entity<TraLoiLienLac>()
					.HasOne(p => p.LienLac);
			
			builder.Entity<SanPham>()
					.HasOne(p => p.MatHang)
					.WithMany(x => x.SanPhames)
					.HasForeignKey(x => x.MatHangId);
			
			builder.Entity<SanPham>()
					.HasOne(p => p.DanhMuc)
					.WithMany(x => x.SanPhames)
					.HasForeignKey(x => x.DanhMucId);
			
			builder.Entity<SanPham>()
					.HasOne(p => p.DongDienThoai)
					.WithMany(x => x.SanPhames)
					.HasForeignKey(x => x.DongDienThoaiId);
            
			builder.Entity<DanhMuc>()
					.HasOne(p => p.MatHang)
					.WithMany(x => x.DanhMuces)
					.HasForeignKey(x => x.MatHangId);

        }
    }
}
