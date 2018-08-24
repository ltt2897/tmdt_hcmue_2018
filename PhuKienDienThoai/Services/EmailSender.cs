using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PhuKienDienThoai.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("phukiendienthoai41@gmail.com", "admin@1234"),
                EnableSsl = true,
            };
            var from = ("phukiendienthoai41@gmail.com");
            var mail = new MailMessage(from, email, subject, message);
            mail.IsBodyHtml = true;
            mail.HeadersEncoding = Encoding.UTF8;
            client.Send(mail);
            return Task.CompletedTask;
        }
    }
}
