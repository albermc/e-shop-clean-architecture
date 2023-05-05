using Application.Orders.Commands.CreateOrder;
using Application.Orders.Queries.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/orders")]
public sealed class OrdersController : ApiController
{

	public OrdersController(ISender sender)
		: base(sender)
	{

	}

	[HttpPost]
	public async Task<IActionResult> RegisterOrder(CancellationToken cancellationToken)
	{
		var command = new CreateOrderCommand(
			Guid.NewGuid());

		var result = await Sender.Send(command, cancellationToken);

		return result.IsSuccess ? Ok() : BadRequest(result.Errors);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetOrderById(Guid id, CancellationToken cancellationToken)
	{
		var query = new GetOrderByIdQuery(id);

		var response = await Sender.Send(query, cancellationToken);

		return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors);
	}
}
