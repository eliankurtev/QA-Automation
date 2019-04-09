namespace Blog.helpers
{
    using Newtonsoft.Json;

    public static class Serialize
    {
        public static string ToJson(this Models.Article self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this Models.ApplicationUser self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
