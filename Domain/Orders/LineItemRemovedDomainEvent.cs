using Domain.Primitives;

namespace Domain.Orders;

public record LineItemRemovedDomainEvent(Guid Id, Guid OrderId, Guid LineItemId)
	: DomainEvent(Id);
