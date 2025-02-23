using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Books;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Books.Create;

internal sealed class CreateBookCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateBookCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Title = command.Title,
            PublicationYear = command.PublicationYear,
            AuthorName = command.AuthorName,
            CreatedAt = dateTimeProvider.UtcNow
        };

        book.Raise(new BookCreatedDomainEvent(book.Id));

        context.Books.Add(book);

        await context.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
