using PC_Designer.ViewModels;

namespace PC_Designer.Client.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly HttpClient _httpClient;

        public DatabaseLogger(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IDisposable? BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            LogViewModel logViewModel = new()
            {
                LogLevel = logLevel.ToString(),
                EventName = eventId.Name,
                ExceptionMessage = exception.Message,
                StackTrace = exception.StackTrace,
                Source = "Client",
                CreatedDate = DateTime.Now.ToString()
            };

            _httpClient.PostAsJsonAsync<LogViewModel>("/logs", logViewModel);
        }
    }
}