namespace Domain.Exceptions;

public sealed class SkuIsNullOrEmptyDomainException : DomainException
{
	public SkuIsNullOrEmptyDomainException(string message) : base(message)
	{

	}
}
