using Bookify.Domain.Abstractions;
using MediatR;

namespace Bookify.Application.Abstraction.Messaging;
public interface ICommand : IRequest<Result>
{
}
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}