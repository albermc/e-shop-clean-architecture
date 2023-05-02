using Application.Abstractions.Messaging;
using Application.Data;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries.GetOrderById;

public sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderResponse>
{
	private readonly IApplicationDbContext _context;

	public GetOrderByIdQueryHandler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
	{
		// Mapping to a DTO in select, make it AsNoTracking by default
		var order = await _context
			.Orders
			.Where(o => o.Id == request.orderId)
			.Select(order => new OrderResponse(
				order.Id,
				order.CustomerId,
				order.LineItems
					.Select(li => new LineItemResponse(li.Id, li.Price.Amount))
					.ToList()))
			.SingleAsync(cancellationToken);

		if (order is null)
			return Result.Failure<OrderResponse>(
				new Error(
					"Order.NotFound",
					$"The order with ID {request.orderId} was not found"));

		return order;
	}
}
