using Domain.Exceptions;

namespace Domain.Products;

// Stock Keeping Unit
public record Sku
{
	private const int DefaultLength = 15;
	private Sku(string value) => Value = value;
	public string Value { get; init; }
	public static Sku Create(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			throw new SkuIsNullOrEmptyDomainException($"{nameof(value)} cannot be null or empty");
		}

		if (value.Length != DefaultLength)
		{
			throw new SkuIsNot15CharsLengthDomainException($"The SKU value needs to have 15 characters length");
		}

		return new Sku(value);
	}
}
