using Produtos.Application.Produtos.Commands;
using Produtos.Application.Produtos.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Produtos.Application.Produtos.Interfaces
{
    public interface IProdutoAppService
    {
        Task<ProdutoCadastradoViewModel> CadastrarProduto(CreateProdutoCommand command);
        Task<IEnumerable<ProdutoViewModel>> PesquisarProdutos(string nome);
        Task<ProdutoViewModel> AtualizarProduto(UpdateProdutoCommand updateProdutoCommand);
        Task<bool> ExcluirProduto(Guid id);
        Task<FileStream> ImagemProduto(Guid produtoId, long idImagem);
    }
}