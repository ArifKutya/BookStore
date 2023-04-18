using BookStore.Models.Models;
using FluentValidation;

namespace BookStore.Validators
{
    public class AddBookValidator : AbstractValidator<Book>
    {
        public AddBookValidator()
        {
            RuleFor(x => x.AuthorId)
                .NotEmpty();
            RuleFor(x => x.Title)
                .NotNull().NotEmpty()
                .MinimumLength(1);
        }

    }
}
