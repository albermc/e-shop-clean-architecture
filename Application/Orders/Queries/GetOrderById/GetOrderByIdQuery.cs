using Application.Abstractions.Messaging;

namespace Application.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(Guid orderId) : IQuery<OrderResponse>
{
}
