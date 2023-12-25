namespace TSR_Accoun_Application.Contracts.User.Responses
{
	public record class UserResponse
	{
		public Guid Id { get; init; }
		public string UserName { get; init; }
		public string Email { get; init; }
		public string PhoneNumber { get; init; }
		public bool EmailConfirmed { get; init; }
		public bool PhoneNumberConfirmed { get; init; }
		public string FullName { get; init; }
	}
}
