﻿namespace RepositoryUnitOfWorkEFCoreSQL.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception innerMesage) : base(message, innerMesage)
    {
    }
}

