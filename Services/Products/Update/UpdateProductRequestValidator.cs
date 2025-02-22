using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products.Update
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Product name is required")
               .NotEmpty().WithMessage("Product name is required")
               .Length(3, 10).WithMessage("Product name must be between 3 and 10 characters");

            //price validation
            RuleFor(x => x.Price).NotNull().WithMessage("Product price is required")
                .GreaterThan(0).WithMessage("Product price must be greater than 0");

            //stock validation
            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 100).WithMessage("Product stock must be between 1 and 100");
        }
    }
}
