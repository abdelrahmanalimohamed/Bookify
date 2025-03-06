using Bookify.Application.Abstraction.Email;

namespace Bookify.Infrastructure.Email
{
	class EmailServices : IEmailService
	{
		public Task SendEmail(Domain.Users.Email email, string subject, string body)
		{
			return Task.CompletedTask;
		}
	}
}
