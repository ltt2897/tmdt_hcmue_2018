using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using PhuKienDienThoai.Services;

namespace PhuKienDienThoai.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link, IHostingEnvironment enviroment = null)
        {
            if (enviroment != null)
            {
                var template = new Repositories.Email.EmailTemplate(enviroment).ConfirmTemplatePath("EmailConfirm");
                using (var StreamReader = System.IO.File.OpenText(template))
                {
                    var body = StreamReader.ReadToEndAsync().Result;
                    body = body.Replace("{ConfirmLink}", HtmlEncoder.Default.Encode(link))
                                        .Replace("{Email}", email);
                    return emailSender.SendEmailAsync(email, "Confirm your email", body);
                }
            }

            return emailSender.SendEmailAsync(email, "Confirm your email",
                    $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
