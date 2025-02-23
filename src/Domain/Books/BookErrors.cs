using SharedKernel;

namespace Domain.Books;

public static class BookErrors
{
    public static Error AlreadyCompleted(Guid bookId) => Error.Problem(
        "Books.AlreadyCompleted",
        $"The book item with Id = '{bookId}' is already completed.");

    public static Error NotFound(Guid bookId) => Error.NotFound(
        "Books.NotFound",
        $"The book with the Id = '{bookId}' was not found");
}
