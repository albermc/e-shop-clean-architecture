using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.CreateOrder
{
	internal sealed class CreatePaymentRequestEventHandler
		: INotificationHandler<OrderCreatedEvent>
	{

		private readonly ILogger<CreatePaymentRequestEventHandler> _logger;

		public CreatePaymentRequestEventHandler(ILogger<CreatePaymentRequestEventHandler> logger)
		{
			_logger = logger;
		}

		public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Starting payment request {@OrderId}", notification.orderId);

			// Simulating order confirmation
			await Task.Delay(2000, cancellationToken);

			_logger.LogInformation("Payment request has started {@OrderId}", notification.orderId);
		}
	}
}
