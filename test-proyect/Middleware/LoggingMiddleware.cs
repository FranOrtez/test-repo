namespace test_proyect.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(
            RequestDelegate next,
            ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path;

            _logger.LogInformation("Request iniciado: {Method} {Path}", method, path);

            var startTime = DateTime.UtcNow;

            await _next(context);

            var elapsedMs = (DateTime.UtcNow - startTime).TotalMilliseconds;

            _logger.LogInformation(
                "Request finalizado: {Method} {Path} - StatusCode: {StatusCode} - Tiempo: {ElapsedMs} ms",
                method,
                path,
                context.Response.StatusCode,
                elapsedMs
            );
        }
    }
}
