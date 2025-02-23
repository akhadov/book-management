using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Books;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Books.Create;

internal sealed class CreateBookCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateBookCommand, List<Guid>>
{
    public async Task<Result<List<Guid>>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        if (command.Books is null || command.Books.Count == 0)
        {
            return Result.Failure<List<Guid>>(BookErrors.NoBooksProvided());
        }

        List<string> existingTitles = await context.Books
            .AsNoTracking()
            .Select(b => b.Title)
            .ToListAsync(cancellationToken);

        var newBooks = command.Books
            .Where(bookDto => !existingTitles.Contains(bookDto.Title))
            .Select(bookDto => new Book
            {
                Id = Guid.NewGuid(),
                Title = bookDto.Title,
                PublicationYear = bookDto.PublicationYear,
                AuthorName = bookDto.AuthorName,
                CreatedAt = dateTimeProvider.UtcNow
            })
            .ToList();

        if (newBooks.Count == 0)
        {
            return Result.Failure<List<Guid>>(BookErrors.AllBooksExist());
        }

        await context.Books.AddRangeAsync(newBooks, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newBooks.Select(b => b.Id).ToList();
    }
}
