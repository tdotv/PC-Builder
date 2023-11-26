namespace PC_Designer.Client.Logging
{
    public class ApplicationLoggerProvider : ILoggerProvider
    {
        private readonly HttpClient _httpClient;

        public ApplicationLoggerProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(_httpClient);
        }

        public void Dispose()
        {
            
        }
    }
}