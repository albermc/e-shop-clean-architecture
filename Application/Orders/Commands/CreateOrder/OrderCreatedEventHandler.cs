using Domain.Orders;
using MediatR;

namespace Application.Orders.Commands.CreateOrder;

internal sealed class OrderCreatedEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
	private readonly IPublisher _publisher;

	public OrderCreatedEventHandler(IPublisher publisher)
	{
		_publisher = publisher;
	}

	public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
	{
		await _publisher.Publish(new OrderCreatedEvent(notification.OrderId), cancellationToken);
	}
}
