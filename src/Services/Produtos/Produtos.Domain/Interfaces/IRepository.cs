using Produtos.Domain.SeedWork;

namespace Produtos.Domain.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}