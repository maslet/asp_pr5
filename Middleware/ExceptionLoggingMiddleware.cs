namespace usov_402_pr5.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;

        public ExceptionLoggingMiddleware(RequestDelegate next, IHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        private void LogError(Exception ex)
        {
            var logsDir = Path.Combine(_environment.ContentRootPath, "logs");
            if (!Directory.Exists(logsDir))
            {
                Directory.CreateDirectory(logsDir);
            }

            var logPath = Path.Combine(logsDir, "error.log");
            var message = $"Time: {DateTime.Now}\nError: {ex.Message}\nStackTrace: {ex.StackTrace}\n\n";

            File.AppendAllText(logPath, message);
        }
    }
}
