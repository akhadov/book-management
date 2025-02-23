using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Books;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Books.Delete;

internal sealed class DeleteBookCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteBookCommand>
{
    public async Task<Result> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
    {
        if (command.BookIds is null || command.BookIds.Count == 0)
        {
            return Result.Failure(BookErrors.NoBooksProvided());
        }

        List<Book>? books = await context.Books
            .Where(book => command.BookIds.Contains(book.Id))
            .ToListAsync(cancellationToken);

        if (books.Count == 0)
        {
            return Result.Failure(BookErrors.NoBooksFound());
        }

        foreach (Book book in books)
        {
            book.Raise(new BookDeletedDomainEvent(book.Id));
        }

        context.Books.RemoveRange(books);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
