using Produtos.Application.Produtos.Commands;
using Produtos.Application.Produtos.Interfaces;
using Produtos.Application.Produtos.ViewModels;
using Produtos.Domain.Interfaces;
using Produtos.Infra.CrossCutting.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.Application.Produtos
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoService _produtoService;
        private readonly IFileService _fileService;
        private const string _imgsFolder = "produtos-imgs";
        private const string _imgsApi = "api/v1/produto/{id}/imagem/{idImagem}";
        private readonly string _host;
        public ProdutoAppService(IProdutoService produtoService, IFileService fileService, string host)
        {
            _produtoService = produtoService;
            _fileService = fileService;
            _host = host;
        }

        public async Task<ProdutoCadastradoViewModel> CadastrarProduto(CreateProdutoCommand command)
        {
            ProdutoCadastradoViewModel viewModelResult = null;

            var result = await _produtoService.CadastrarProduto(command.Nome, command.Valor, command.Imagem.Conteudo.FileName);

            if (result != null)
            {
                viewModelResult = new ProdutoCadastradoViewModel()
                {
                    Id = result.Id,
                    Nome = result.Nome,
                    Valor = result.Valor
                };

                _fileService.SaveFileAsync(command.Imagem.Conteudo, _imgsFolder, result.Id.ToString());
            }

            return viewModelResult;
        }

        private string ImgUrl(Guid idProduto, long idImagem)
        {
            return _imgsApi.Replace("{id}", idProduto.ToString()).Replace("{idImagem}", idImagem.ToString());
        }

        public async Task<IEnumerable<ProdutoViewModel>> PesquisarProdutos(string nome)
        {
            var result = await _produtoService.PesquisarProdutos(nome);

            return result.Select(x => new ProdutoViewModel()
            {
                Id = x.Id,
                Imagens = x.Imagens.Select(y => $"{_host}/{ImgUrl(y.ProdutoId, y.Id)}"),
                Nome = x.Nome,
                Valor = x.Valor
            });
        }

        public async Task<ProdutoViewModel> AtualizarProduto(UpdateProdutoCommand updateProdutoCommand)
        {
            ProdutoViewModel produtoViewModel = null;

            var produto = await _produtoService.GetById(updateProdutoCommand.Id);

            if (produto != null)
            {
                produto.AtualizarProduto(updateProdutoCommand.Nome, updateProdutoCommand.Valor);

                var result = await _produtoService.AtualizarProduto(produto);

                if (result != null)
                    produtoViewModel = new ProdutoViewModel()
                    {
                        Id = result.Id,
                        Nome = result.Nome,
                        Valor = result.Valor
                    };
            }

            return produtoViewModel;
        }

        public async Task<bool> ExcluirProduto(Guid id)
        {
            var produto = await _produtoService.GetById(id);

            if (produto != null)
                return await _produtoService.ExcluirProduto(produto);

            return false;
        }

        public async Task<FileStream> ImagemProduto(Guid produtoId, long idImagem)
        {
            FileStream result = null;
            var imagem = await _produtoService.GetProdutoImagemById(produtoId, idImagem);

            if (imagem != null)
            {
                ImagemViewModel file = new ImagemViewModel()
                {
                    Id = imagem.Id,
                    ProdutoId = imagem.ProdutoId,
                    Nome = imagem.Nome
                };

                result = _fileService.ReadFile(_imgsFolder, file.NomeImagemSalva);
            }

            return result;
        }
    }
}