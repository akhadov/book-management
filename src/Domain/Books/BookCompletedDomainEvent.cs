using SharedKernel;

namespace Domain.Books;

public sealed record BookCompletedDomainEvent(Guid BookId) : IDomainEvent;
