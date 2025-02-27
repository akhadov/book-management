﻿using FluentValidation;

namespace Application.Books.Create;

public class CreateBookCommandValidator : AbstractValidator<BookDto>
{
    public CreateBookCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.PublicationYear).NotEmpty();
        RuleFor(c => c.AuthorName).NotEmpty();
    }
}
