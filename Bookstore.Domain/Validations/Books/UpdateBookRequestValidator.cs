using Bookstore.Contract.Requests.Book;
using FluentValidation;

namespace Bookstore.Domain.Validations.Books
{
    public class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(B => B.Title)
                .NotEmpty().WithMessage("Title name is required")
                .MinimumLength(3).WithMessage("Title name must be at least 3 characters long")
                .MaximumLength(40).WithMessage("Title name must be at most 40 characters long");

            RuleFor(B => B.Author)
                .NotEmpty().WithMessage("Author name is required")
                .MinimumLength(3).WithMessage("Author name must be at least 3 characters long")
                .MaximumLength(20).WithMessage("Author name must be at most 20 characters long");

            RuleFor(B => B.Description)
                .NotEmpty().WithMessage("Description is required")
                .MinimumLength(3).WithMessage("Description must be at least 3 characters long")
                .MaximumLength(100).WithMessage("Description must be at most 100 characters long");

            RuleFor(B => B.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(B => B.GenreId)
                .NotEmpty().WithMessage("Genre is required");
        }
    }
}
