using SharedKernel;

namespace Domain.Books;

public sealed record BookDeletedDomainEvent(Guid BookId) : IDomainEvent;
