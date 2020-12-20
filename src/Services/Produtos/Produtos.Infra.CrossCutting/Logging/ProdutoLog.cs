using Microsoft.Extensions.Logging;

namespace Produtos.Infra.CrossCutting.Logging
{
    public class ProdutoLog<T> : IProdutoLog<T> where T : class
    {
        private readonly ILogger _logger;

        public ProdutoLog()
        {
            _logger = new LoggerFactory().CreateLogger<T>();
        }

        public void LogError(string message)
        {
            _logger.LogError(message);
        }

        public void LogTrace(string message)
        {
            _logger.LogTrace(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }
    }
}