using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Books;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Books.Update;
internal sealed class UpdateBookCommandHandler(IApplicationDbContext context) : ICommandHandler<UpdateBookCommand>
{
    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        Book? book = await context.Books.SingleOrDefaultAsync(x => x.Id == request.BookId, cancellationToken);

        if (book is null)
        {
            return Result.Failure(BookErrors.NotFound(request.BookId));
        }

        book.Title = request.Title;
        book.PublicationYear = request.PublicationYear;
        book.AuthorName = request.AuthorName;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
