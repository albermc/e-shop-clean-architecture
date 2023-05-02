using Domain.Products;

namespace Domain.Orders;

public class LineItem
{
	public LineItem(Guid id, Guid orderId, Guid productId, Money price)
	{
		Id = id;
		OrderId = orderId;
		ProductId = productId;
		Price = price;
	}

	public Guid Id { get; private set; }
	public Guid OrderId { get; private set; }
	public Guid ProductId { get; private set; }
	public Money Price { get; private set; }
}
