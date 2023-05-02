namespace Application.Orders.Queries.GetOrderById;

public record LineItemResponse(Guid LineItemId, decimal Price);