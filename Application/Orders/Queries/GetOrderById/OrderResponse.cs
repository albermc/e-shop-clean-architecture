namespace Application.Orders.Queries.GetOrderById;

public sealed record OrderResponse(Guid Id, Guid customerId, List<LineItemResponse> lineItems);
