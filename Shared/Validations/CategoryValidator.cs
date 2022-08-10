using FluentValidation;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description is Required");
        }
    }

    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description is Required");
        }
    }
}
