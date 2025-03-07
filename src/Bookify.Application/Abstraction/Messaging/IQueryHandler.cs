﻿using Bookify.Domain.Abstractions;
using MediatR;

namespace Bookify.Application.Abstraction.Messaging;
public interface IQueryHandler<TQuery , TResponse> : IRequestHandler<TQuery , Result<TResponse>>
	where TQuery : IQuery<TResponse>
{
}