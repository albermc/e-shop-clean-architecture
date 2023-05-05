using Domain.Exceptions;
using Domain.Products;
using System.ComponentModel;

namespace Domain.UnitTests.Products;

public class SkuTests
{
	// [ThingUnderTest]_Should_[ExpectedResult]_[Condition]
	[Theory]
	[InlineData("")]
	[InlineData(null)]
	public void Create_Should_ThrowException_WhenValueIsNull(string? value)
	{
		// Act & Assert
		Assert.Throws<SkuIsNullOrEmptyDomainException>(() => Sku.Create(value!));
	}

	public static IEnumerable<object[]> InvalidSkuLengthData = new List<object[]>
	{
		new object[] { "invalid_sku" },
		new object[] { "invalid_sku_1" },
		new object[] { "invalid_sku_2" },
	};

	[Theory]
	[MemberData(nameof(InvalidSkuLengthData))]
	public void Create_Should_ThrowException_WhenValueLengthIsInvalid(string value)
	{
		// Act & Assert
		Assert.Throws<SkuIsNot15CharsLengthDomainException>(() => Sku.Create(value!));
	}
}
