namespace Domain.Exceptions;

public sealed class SkuIsNot15CharsLengthDomainException : DomainException
{
	public SkuIsNot15CharsLengthDomainException(string message) : base(message)
	{
	}
}
