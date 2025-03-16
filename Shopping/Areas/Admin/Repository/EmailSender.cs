using System.Net;
using System.Net.Mail;

namespace Shopping_Tutorial.Areas.Admin.Repository
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true, //bật bảo mật
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("duongmanhhung1210@gmail.com", "ezqwzdcxgfbczhad")
            };

            return client.SendMailAsync(
                new MailMessage(from: "duongmanhhung1210@gmail.com",
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}