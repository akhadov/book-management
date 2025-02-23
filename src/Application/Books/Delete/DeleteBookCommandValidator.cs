using FluentValidation;

namespace Application.Books.Delete;

internal sealed class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(c => c.BookId).NotEmpty();
    }
}
