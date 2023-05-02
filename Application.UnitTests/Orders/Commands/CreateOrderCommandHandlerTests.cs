using Application.Data;
using Application.Orders.Commands.CreateOrder;
using Domain.Customers;
using Domain.Orders;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests.Orders.Commands;

public class CreateOrderCommandHandlerTests
{

	private readonly Mock<IApplicationDbContext> _dbContextMock;

	public CreateOrderCommandHandlerTests()
	{
		_dbContextMock = new Mock<IApplicationDbContext>();
	}

	[Fact]
	public async Task Handle_Should_ReturnFailure_WhenCustomerDoesNotExist()
	{
		// Arrange
		var command = new CreateOrderCommand(Guid.NewGuid());

		// For mocking a certain method
		//_dbContextMock.Setup(
		//	x => x.Customers)
		//	.Returns(new DbSet<Customer>());

		var handler = new CreateOrderCommandHandler(_dbContextMock.Object);

		// Act
		var result = await handler.Handle(command, default);

		// Assert
		result.IsFailure.Should().BeTrue();
		//result.Error.Should().Be();

	}

	[Fact]
	public async Task Handle_Should_ReturnSuccess_WhenCustomerExists()
	{
		// Arrange
		var command = new CreateOrderCommand(Guid.NewGuid());

		// For mocking a certain method
		//_dbContextMock.Setup(
		//	x => x.Equals,
		//	It.IsAny<Order>(),
		//	It.IsAny<CancellationToken>())
		//	.Returns(false);

		var handler = new CreateOrderCommandHandler(_dbContextMock.Object);

		// Act
		var result = await handler.Handle(command, default);

		// Assert
		result.IsSuccess.Should().BeTrue();
		result.Value.Should().NotBeEmpty();
	}

	[Fact]
	public async Task Handle_Should_CallOnAdd_WhenCustomerExists()
	{
		// Arrange
		var command = new CreateOrderCommand(Guid.NewGuid());

		var handler = new CreateOrderCommandHandler(_dbContextMock.Object);

		// Act
		var result = await handler.Handle(command, default);

		// Assert
		_dbContextMock.Verify(
			x => x.Orders.Add(It.Is<Order>(o => o.Id == result.Value)),
			Times.Once());
	}

	[Fact]
	public async Task Handle_Should_NotCallSaveChangesAsync_WhenCustomerNotExists()
	{
		// Arrange
		var command = new CreateOrderCommand(Guid.NewGuid());

		var handler = new CreateOrderCommandHandler(_dbContextMock.Object);

		// Act
		await handler.Handle(command, default);

		// Assert
		_dbContextMock.Verify(
			x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
			Times.Never());
	}
}
