using Newtonsoft.Json;

namespace Blog.UITests.Models
{
	public static class Serialize
	{
		public static string ToJson(this ChangePassword self) => JsonConvert.SerializeObject(self, Converter.Settings);
		public static string ToJson(this User self) => JsonConvert.SerializeObject(self, Converter.Settings);
	}
}
