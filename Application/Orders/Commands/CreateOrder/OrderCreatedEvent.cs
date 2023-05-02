using MediatR;

namespace Application.Orders.Commands.CreateOrder;

public record OrderCreatedEvent(Guid orderId) : INotification
{
}
