using FluentValidation;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    { 
        RuleFor(_ => _.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(_ => _.Description)
            .NotEmpty().WithMessage("Product description is required.")
            .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");

        RuleFor(_ => _.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(_ => _.CategoryId)
            .NotEmpty().WithMessage("Category Id is required.");
    }
}
