using Domain.Shared;

namespace Domain.Errors;

public static class DomainErrors
{
	public static class Email
	{
		public static readonly Error Empty = new Error("Email.Empty", "Email is empty");
		public static readonly Error TooLong = new Error("Email.TooLong", "Email is too long");
		public static readonly Error InvalidFormat = new Error("Email.Invalid", "Email has an invalid format");
	}
}
