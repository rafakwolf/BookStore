using System.Threading.Tasks;

namespace BookStore.Domain.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
