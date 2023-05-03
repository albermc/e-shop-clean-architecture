using Domain.Primitives;

namespace Domain.Orders;

public record class OrderCreatedDomainEvent(Guid Id, Guid OrderId) : DomainEvent(Id);
