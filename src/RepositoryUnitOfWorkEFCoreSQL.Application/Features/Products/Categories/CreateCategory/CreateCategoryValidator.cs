﻿using FluentValidation;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.Categories.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(_ => _.Name).NotNull().NotEmpty().WithMessage("Please input category name!");
        RuleFor(_ => _.Name).NotNull().WithMessage("Please input category description!");
    }
}
