namespace Bookify.Application.Abstraction.Email;

public interface IEmailService
{
    Task SendEmail(Domain.Users.Email email, string subject, string body);
}