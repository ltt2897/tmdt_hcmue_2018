using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PhuKienDienThoai.Bussinesses
{
    public class RoleBussiness
    {
        readonly  RoleManager<IdentityRole> _roleManager;
        public RoleBussiness(RoleManager<IdentityRole> roleManager) => _roleManager = roleManager;
        ///<summary>
        ///hàm thêm Role Identity vào csdl
        ///</summary>
        ///<param name='_identity'>Identity cần thêm</param> 
        ///<br/>
        ///<returns>true nếu thêm role vào thành công</returns>
        ///<br/>
        ///<returns>false nếu có lỗi trong quá trình thực hiện</returns>      
        public async Task<bool> Create(IdentityRole _identity)
        {
            try
            {
                var KiemTraRoleTonTai = await _roleManager.RoleExistsAsync(_identity.Name);
                if (!KiemTraRoleTonTai)
                    await _roleManager.CreateAsync(_identity);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}