﻿using Application.Abstractions.Messaging;
using Application.Data;
using Domain.Orders;
using Domain.Shared;
using MediatR;

namespace Application.Orders.Commands.CreateOrder
{
	internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
	{
		private readonly IApplicationDbContext _context;

		public CreateOrderCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var customer = await _context.Customers.FindAsync(request.CustomerId);

			if (customer is null)
				return (Result<Guid>)Result.Failure(
					new Error(
						"Customer does not exist",
						$"Customer with ID = {request.CustomerId} does not exist"
					));

			var order = Order.Create(customer);

			_context.Orders.Add(order);

			await _context.SaveChangesAsync(cancellationToken);

			return Result.Success(order.Id);
		}

	}
}
