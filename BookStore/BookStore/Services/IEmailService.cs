using BookStore.Models;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions emailOptions);
        Task SendConfirmationEmail(UserEmailOptions emailOptions);
        Task SendForgotPasswordEmail(UserEmailOptions emailOptions);
    }
}