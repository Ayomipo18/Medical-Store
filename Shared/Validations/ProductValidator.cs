using FluentValidation;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validations
{
    public class ProductValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage("Category Id is Required");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description is Required");
            RuleFor(x => x.CostPrice).NotNull().NotEmpty().WithMessage("Cost Price is Required");
            RuleFor(x => x.ProfitMargin).NotNull().NotEmpty().WithMessage("Profit Margin is Required");
            RuleFor(x => x.Quantity).NotNull().NotEmpty().WithMessage("Quantity is Required");
        }
    }

    public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage("Category Id is Required");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description is Required");
            RuleFor(x => x.CostPrice).NotNull().NotEmpty().WithMessage("Cost Price is Required");
            RuleFor(x => x.ProfitMargin).NotNull().NotEmpty().WithMessage("Profit Margin is Required");
            RuleFor(x => x.Quantity).NotNull().NotEmpty().WithMessage("Quantity is Required");
        }
    }
}
