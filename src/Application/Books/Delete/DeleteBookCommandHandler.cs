using Application.Abstractions.Authentication;
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
        Book? book = await context.Books
            .SingleOrDefaultAsync(t => t.Id == command.BookId, cancellationToken);

        if (book is null)
        {
            return Result.Failure(BookErrors.NotFound(command.BookId));
        }

        context.Books.Remove(book);

        book.Raise(new BookDeletedDomainEvent(book.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
