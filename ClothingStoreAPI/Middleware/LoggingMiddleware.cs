using System.Diagnostics;

namespace ClothingStoreAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
               
                _logger.LogInformation($"Início da requisição: {context.Request.Path} - {DateTime.UtcNow}");
                _logger.LogWarning($"Origem da Requisição: {context.Request.Headers["Origin"]}");
                await _next(context);
               
                stopwatch.Stop();
                _logger.LogInformation($"Fim da requisição: {context.Request.Path} - {DateTime.UtcNow} - Tempo total: {stopwatch.ElapsedMilliseconds}ms");
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, $"Erro na requisição: {context.Request.Path} - {DateTime.UtcNow} - Tempo total: {stopwatch.ElapsedMilliseconds}ms - Erro: {ex.Message}");
                throw;
            }
        }
    }
}
