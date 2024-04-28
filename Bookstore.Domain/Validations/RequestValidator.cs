using FluentValidation;
using FluentValidation.Results;

namespace Bookstore.Domain.Validations
{
    public class RequestValidator<T> : AbstractValidator<T>
    {
        public RequestValidator()
        {
            
        }

        public async Task<ValidationResult> ValidationAsync(T request)
        {
            var context = new ValidationContext<T>(request);
            var result = await ValidateAsync(context);
            return result;
        }

    }
}
