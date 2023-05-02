using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.CreateOrder
{
	internal sealed class SendOrderConfirmationEventHandler
		: INotificationHandler<OrderCreatedEvent>
	{

		private readonly ILogger<SendOrderConfirmationEventHandler> _logger;

		public SendOrderConfirmationEventHandler(ILogger<SendOrderConfirmationEventHandler> logger)
		{
			_logger = logger;
		}

		public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Sending order confirmation {@OrderId}", notification.orderId);

			// Simulating order confirmation
			await Task.Delay(2000, cancellationToken);

			_logger.LogInformation("Order confirmation sent {@OrderId}", notification.orderId);
		}
	}
}
