using Bookstore.Contract.Requests.Genre;
using FluentValidation;

namespace Bookstore.Domain.Validations.Genres
{
    public class CreateGenreRequestValidator : AbstractValidator<CreateGenreRequest>
    {
        public CreateGenreRequestValidator()
        {
            RuleFor(G => G.Name)
                .NotEmpty().WithMessage("Genre name is required")
                .MinimumLength(3).WithMessage("Genre name must be at least 3 characters long")
                .MaximumLength(20).WithMessage("Genre name must be at most 20 characters long");
        }        
    }
}
