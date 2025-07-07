﻿namespace RepositoryUnitOfWorkEFCoreSQL.Application.Common.Mediator;

public interface IMediator
{
    Task Send(IRequest request, CancellationToken cancellationToken = default);
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
