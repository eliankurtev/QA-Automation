namespace Blog.UITests.Models
{
	using Newtonsoft.Json;

	public class User
	{
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("fullName")]
		public string FullName { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		[JsonProperty("confirmPassword")]
		public string ConfirmPassword { get; set; }

		public static User UserFromJson(string json) => JsonConvert.DeserializeObject<User>(json, Converter.Settings);
	}

}