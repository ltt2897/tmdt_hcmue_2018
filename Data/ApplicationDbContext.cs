using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Sach> Sach { get; set; }
        public DbSet<TacGia> TacGia { get; set; }
        public DbSet<NhaXuatBan> NhaXuatBan { get; set; }
        public DbSet<NhanXet> NhanXet { get; set; }
        public DbSet<DanhMuc> DanhMuc { get; set; }
        public DbSet<ChuDe> ChuDe { get; set; }
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
			builder.Entity<Sach>().HasKey(p => p.id);
			builder.Entity<ChiTietHoaDon>()
					.HasKey(p => new { p.HoaDonId, p.SachId });

			builder.Entity<ChiTietHoaDon>()
					.HasOne(p => p.Sach)
					.WithMany(p => p.ChiTietHoaDons)
					.HasForeignKey(p => p.SachId);

			builder.Entity<ChiTietHoaDon>()
					.HasOne(p => p.HoaDon)
					.WithMany(p => p.ChiTietHoaDons)
					.HasForeignKey(p => p.HoaDonId);

			builder.Entity<Wishlist>()
					.HasKey(p => new { p.SachID, p.UserID });

			builder.Entity<Wishlist>()
					.HasOne(p => p.Sach)
					.WithMany(p => p.Wishlist)
					.HasForeignKey(p => p.SachID);

			builder.Entity<Wishlist>()
					.HasOne(p => p.User)
					.WithMany(p => p.Wishlist)
					.HasForeignKey(p => p.UserID);

			builder.Entity<TraLoiLienLac>()
					.HasOne(p => p.LienLac);
			
			builder.Entity<Sach>()
					.HasOne(p=>p.ChuDe)
					.WithMany(x=>x.Saches)
					.HasForeignKey(x=>x.ChuDeId);
			
			builder.Entity<Sach>()
					.HasOne(p=>p.DanhMuc)
					.WithMany(x=>x.Saches)
					.HasForeignKey(x=>x.DanhMucId);
			
			builder.Entity<Sach>()
					.HasOne(p=>p.TacGia)
					.WithMany(x=>x.Saches)
					.HasForeignKey(x=>x.TacGiaId);
                
        }
    }
}
