using Produtos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produtos.Domain.Interfaces
{
    public interface IProdutoService
    {
        Task<Produto> GetById(Guid id);
        Task<Produto> CadastrarProduto(string nome, decimal valor, string nomeImagem);
        Task<IEnumerable<Produto>> PesquisarProdutos(string nome);
        Task<Produto> AtualizarProduto(Produto produto);
        Task<bool> ExcluirProduto(Produto produto);
        Task<Imagem> GetProdutoImagemById(Guid produtoId, long imagemId);
    }
}