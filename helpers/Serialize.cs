namespace Blog.helpers
{
    using Newtonsoft.Json;

    public static class Serialize
    {
        public static string ToJson(this Models.RegistrationUser self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
