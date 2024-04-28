using Bookstore.Contract.Requests.User;
using FluentValidation;
using System.Text.RegularExpressions;
namespace Bookstore.Domain.Validations.Auth
{
    public class CreateUserRequestValidator : RequestValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(U => U.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters")
                .MaximumLength(20).WithMessage("Username must be less than 20 characters")
                .Matches("^[^\\s]+$", RegexOptions.IgnoreCase) 
                .WithMessage("Username cannot contain spaces");

            RuleFor(U => U.Fullname)
                .NotEmpty().WithMessage("Fullname is required")
                .MinimumLength(3).WithMessage("Fullname must be at least 3 characters")
                .MaximumLength(50).WithMessage("Fullname must be less than 50 characters");

            RuleFor(U => U.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");                
        
        }
    }
}
