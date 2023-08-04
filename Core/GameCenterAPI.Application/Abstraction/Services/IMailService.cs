namespace GameCenterAPI.Application.Abstraction.Services
{
    public interface IMailService
    {
        Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
        Task SendEmailConfirmMailAsync(string to, string userId, string emailConfirmationToken);

    }
}
