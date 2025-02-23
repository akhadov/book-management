using SharedKernel;

namespace Domain.Books;

public sealed record BookCreatedDomainEvent(Guid BookId) : IDomainEvent;
