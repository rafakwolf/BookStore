using System.Threading.Tasks;

namespace BookStore.Domain.Contracts
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
