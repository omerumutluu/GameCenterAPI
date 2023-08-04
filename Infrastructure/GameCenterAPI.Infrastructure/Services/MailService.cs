using GameCenterAPI.Application.Abstraction.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace GameCenterAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new("oyungine@gmail.com", "Game Center Mail", Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = Convert.ToInt32(_configuration["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];

            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            string message = $"Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz<br><br><strong><a target=\"_blank\" href=\"{_configuration["VueClientUrl"]}/update-password/{userId}/{resetToken}\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleşmediyse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br> <strong>Game Center Hesap ve İtem Satış Hizmetleri</strong>";

            await SendMessageAsync(to, "Game Center Şifre Sıfırlama Talebi", message);
        }

        public async Task SendEmailConfirmMailAsync(string to, string userId, string emailConfirmationToken)
        {
            string message = $"Merhaba<br> Öncelikle Game Center Oyun ve İtem Satış sistemine kayıt olduğunuz için teşekkür ederiz.<br><br>Kaydınızın tamamlanması için aşağıdaki linke tıklayarak mailinizi onaylamanız yeterlidir. <br><br><strong><a target=\"_blank\" href=\"{_configuration["VueClientUrl"]}/verify-email/{userId}/{emailConfirmationToken}\">Hesabımı Onayla</a></strong><br><br><span style=\"font-size:12px;\">NOT : Bu işlem tarafınızca gerçekleşmediyse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br> <strong>Game Center Hesap ve İtem Satış Hizmetleri</strong>";

            await SendMessageAsync(to, "Game Center Hesap Doğrulama", message);
        }
    }
}
