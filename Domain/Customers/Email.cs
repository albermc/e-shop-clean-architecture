using Domain.Errors;
using Domain.Shared;

namespace Domain.Customers;

public class Email
{
	public Email(string email)
	{
		Value = email;
	}

	public string Value { get; set; }
	private const int MaxLength = 255;

	public static Result<Email> Create(string email)
	{
		return Result.Ensure(
			email,
			(e => !string.IsNullOrEmpty(e), DomainErrors.Email.Empty),
			(e => e.Length <= MaxLength, DomainErrors.Email.TooLong),
			(e => e.Split('@').Length == 2, DomainErrors.Email.InvalidFormat))
			.Map(e => new Email(e));

	}

	//public override IEnumerable<object> GetAtomicValues()
	//{
	//	yield return Value;
	//}
}
