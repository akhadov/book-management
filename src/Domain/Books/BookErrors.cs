using SharedKernel;

namespace Domain.Books;

public static class BookErrors
{
    public static Error AlreadyExict(string title) => Error.Problem(
        "Books.AlreadyExict",
        $"The book with title = '{title}' is already exict.");

    public static Error NotFound(Guid bookId) => Error.NotFound(
        "Books.NotFound",
        $"The book with the Id = '{bookId}' was not found");

    public static Error NoBooksProvided() => Error.Problem(
        "Books.NoBooksProvided",
        "No books were provided for creation.");

    public static Error AllBooksExist() => Error.Problem(
        "Books.AllBooksExist",
        "All provided books already exist.");

    public static Error NoBooksFound() => Error.Problem(
        "Books.NoBooksFound",
        "No matching books were found for deletion.");
}
