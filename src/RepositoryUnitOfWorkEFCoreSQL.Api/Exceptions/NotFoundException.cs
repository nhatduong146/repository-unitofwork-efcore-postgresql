﻿namespace RepositoryUnitOfWorkEFCoreSQL.Api.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}
