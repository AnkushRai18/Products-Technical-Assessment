using FluentValidation;
using Products.APPLICATION.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.APPLICATION.Validators;

public class CreateProductValidator
    : AbstractValidator<ProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .MaximumLength(255);
    }
}
