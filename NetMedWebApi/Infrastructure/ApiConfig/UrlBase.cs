namespace NetMedWebApi.Infrastructure.ApiConfig
{
    public static class UrlBase
    {
        public static string GetBaseUrl()
        {
            var confing = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return confing["BaseUrl"];
        
        
        }


    }
}
