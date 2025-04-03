namespace NetMed.WebApi.ConfiUrl
{
    public static class ApiConfig
    {
        public static string GetBaseUrl()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return config["ApiSettings:BaseUrl"];
        }
    }
}
