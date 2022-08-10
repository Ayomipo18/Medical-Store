using FluentValidation;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validations
{
    public class UserValidator : AbstractValidator<UserCreateDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First Name is Required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last Name is Required");
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Email is Required");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is Required");
            RuleFor(x => x.Password)
                .Matches(@"(?-i)(?=^.{8,}$)((?!.*\s)(?=.*[A-Z])(?=.*[a-z]))((?=(.*\d){1,})|(?=(.*\W){1,}))^.*$")
                .WithMessage("Password must be at least 8 characters, have at least 1 upper case letter (A – Z), 1 lower case letter (a – z), 1 number (0 – 9) and 1 non-alphanumeric symbol (e.g. @ '$%£! ')");
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().WithMessage("Phone Number is Required");
        }
    }

    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty().WithMessage("Email cannot be null or empty");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password cannot be null or empty");
        }
    }
}
