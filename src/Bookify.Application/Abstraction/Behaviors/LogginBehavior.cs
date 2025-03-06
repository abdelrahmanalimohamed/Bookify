using Bookify.Application.Abstraction.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookify.Application.Abstraction.Behaviors;
public class LogginBehavior<TRequest, TResponse>
	: IPipelineBehavior<TRequest, TResponse>
	where TRequest : IBaseCommand
{
	private readonly ILogger<TRequest> logger;
	public LogginBehavior(ILogger<TRequest> logger)
	{
		this.logger = logger;
	}

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next, 
		CancellationToken cancellationToken)
	{
		var name = request.GetType().Name;

		try
		{

			logger.LogInformation("Execution command  {Command}", name);

			var result = await next();

			logger.LogInformation("Command {Command} processed successfully ", name);

			return result;
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Command {Command} processing failed", name);
			throw;
		}
	}
}
