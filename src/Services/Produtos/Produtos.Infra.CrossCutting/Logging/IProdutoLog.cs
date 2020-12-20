namespace Produtos.Infra.CrossCutting.Logging
{
    public interface IProdutoLog<T> where T : class
    {
        public void LogError(string message);
        public void LogTrace(string message);
        public void LogWarning(string message);
    }
}