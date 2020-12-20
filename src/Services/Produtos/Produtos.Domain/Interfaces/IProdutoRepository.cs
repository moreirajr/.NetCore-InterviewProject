using Produtos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produtos.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> GetById(Guid id);
        Task<Produto> CadastrarProduto(Produto produto);
        Task<IEnumerable<Produto>> PesquisarProdutos(string nome);
        Produto AtualizarProduto(Produto produto);
        bool ExcluirProduto(Produto produto);
        Task<Imagem> GetProdutoImagemById(Guid produtoId, long imagemId);
    }
}