using Domain.Customers;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders;

public class Order
{
	private readonly HashSet<LineItem> _lineItems = new HashSet<LineItem>();

	private Order()
	{

	}

	public Guid Id { get; private set; }
	public Guid CustomerId { get; private set; }
	public IReadOnlyList<LineItem> LineItems => _lineItems.ToList();

	public void Add(Product product)
	{
		_lineItems.Add(new LineItem(Guid.NewGuid(), Id, product.Id, product.Price));
	}

	public static Order Create(Customer customer)
	{
		Order order = new Order
		{
			Id = Guid.NewGuid(),
			CustomerId = customer.Id
		};

		return order;
	}
}
