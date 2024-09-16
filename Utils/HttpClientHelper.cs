namespace beakiebot_server.Utils
{
    public static class HttpClientHelper
    {
        private static readonly HttpClient client = new();

        public static HttpClient GetClient()
        {
            return client;
        }
    }
}
