using Microsoft.AspNetCore.Hosting;

namespace PhuKienDienThoai.Repositories.Email
{
    public class EmailTemplate
    {
        private IHostingEnvironment environment;

        ///<summary>đường dẫn đến file template</summary>
        ///<param name="enviroment">environment dùng để lấy thông tin môi trường mà trang web đang chạy.</param>
        ///<param name="templateName">Tên của template</param>
        ///<param name="extention">đuôi của template, mặc định là html</param>
        public EmailTemplate(IHostingEnvironment environment)
        {
            this.environment = environment;
        }
        public string ReceiptTemplatePath(string templateName, string extention = "html")
        {
            var _p = environment.WebRootPath
                                + System.IO.Path.DirectorySeparatorChar
                                + "EmailTemplates"
                                + System.IO.Path.DirectorySeparatorChar
                                + "ReceiptContent"
                                + System.IO.Path.DirectorySeparatorChar
                                + templateName + "." + extention;
            return _p;
        }

        public string ConfirmTemplatePath(string templateName, string extention = "html")
        {
            var p = environment.WebRootPath
                                + System.IO.Path.DirectorySeparatorChar + "EmailTemplate"
                                + System.IO.Path.DirectorySeparatorChar + "EmailConfirm" + "." + extention;
            return p;
        }
    }
}