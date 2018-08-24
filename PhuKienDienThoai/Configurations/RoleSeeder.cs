using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace PhuKienDienThoai.Configurations
{
    public class RoleSeeder : IDisposable
    {
        readonly RoleManager<IdentityRole> _roleManager;
        //public RoleSeeder(IServiceProvider RoleManager<IdentityRole> roleManager) => _roleManager = roleManager;
        public RoleSeeder(IServiceProvider _ServiceProvider) =>
            _roleManager = _ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        public void Dispose() => _roleManager.Dispose();
        public async Task SeedAsync()
        {
            var roleBussiness = new Bussinesses.RoleBussiness(_roleManager);
            //Mảng tên các role mặc định
            //user là người mua hàng
            string[] roleNames = { "User", "Admin", "Giao Hàng" };
            foreach (var item in roleNames)
            {
                var ChckRoleExist = await _roleManager.RoleExistsAsync(item);
                if (!ChckRoleExist)
                    await roleBussiness.Create(new IdentityRole { Name = item });
            }
        }
    }
}