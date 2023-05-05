using Domain.Customers;
using Domain.Orders;

namespace Domain.UnitTests.Orders;

public class OrderTests
{
	[Theory]
	[ClassData(typeof(OrderCreateTestData))]
	public void Create_Should_RaiseDomainEvent(Customer customer)
	{
		// Act
		var order = Order.Create(customer);

		// Assert
		Assert.NotEmpty(order.GetDomainEvents().OfType<OrderCreatedDomainEvent>());
	}
}

public class OrderCreateTestData : TheoryData<Customer>
{
	public OrderCreateTestData()
	{
		Add(new Customer());
	}
}

