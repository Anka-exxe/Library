
namespace Library.Application.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string message, string subject, string recieverEmail);
    }
}
