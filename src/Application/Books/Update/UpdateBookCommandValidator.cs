using FluentValidation;

namespace Application.Books.Update;
public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.PublicationYear).NotEmpty();
        RuleFor(c => c.AuthorName).NotEmpty();
    }
}
