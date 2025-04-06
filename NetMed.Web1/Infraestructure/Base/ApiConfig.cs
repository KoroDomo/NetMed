namespace NetMed.ApiConsummer.Infraestructure.Base
{
    public static class ApiConfig
    {
        public static string GetBaseUrl()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return config["ApiBaseUrl"];
        }


    }
}
