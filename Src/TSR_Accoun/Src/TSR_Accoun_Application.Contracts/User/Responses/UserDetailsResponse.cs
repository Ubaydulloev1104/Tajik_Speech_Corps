namespace TSR_Accoun_Application.Contracts.User.Responses
{
	public class UserDetailsResponse
	{
		public bool IsUserNameTaken { get; set; }
		public bool IsPhoneNumberTaken { get; set; }
		public bool IsEmailTaken { get; set; }
	}
}
