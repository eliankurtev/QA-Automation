namespace Blog.UITests.Models
{
    using Newtonsoft.Json;

    public partial class ChangePassword
	{
		[JsonProperty("currentPassword")]
		public string CurrentPassword { get; set; }

		[JsonProperty("newPassword")]
		public string NewPassword { get; set; }

		[JsonProperty("confirmNewPassword")]
		public string ConfirmNewPassword { get; set; }

		public static ChangePassword ChangePasswordFromJson(string json) => JsonConvert.DeserializeObject<ChangePassword>(json, Converter.Settings);
    }

}
