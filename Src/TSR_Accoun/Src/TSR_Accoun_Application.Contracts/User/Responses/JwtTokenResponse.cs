namespace TSR_Accoun_Application.Contracts.User.Responses
{
	public class JwtTokenResponse
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public DateTime AccessTokenValidateTo { get; set; }
	}
}
