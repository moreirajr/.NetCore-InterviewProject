using Produtos.Domain.Interfaces;
using Produtos.Domain.Models;
using Produtos.Infra.CrossCutting.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Produtos.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoLog<ProdutoService> _logger;
        public ProdutoService(IProdutoRepository produtoRepository, IProdutoLog<ProdutoService> logger)
        {
            _produtoRepository = produtoRepository;
            _logger = logger;
        }

        public async Task<Produto> GetById(Guid id)
        {
            return await _produtoRepository.GetById(id);
        }

        public async Task<Produto> CadastrarProduto(string nome, decimal valor, string nomeImagem)
        {
            try
            {
                var produto = new Produto(nome, valor);
                var imagem = new Imagem(nomeImagem);

                produto.AdicionarImagem(imagem);

                var result = await _produtoRepository.CadastrarProduto(produto);
                await _produtoRepository.UnitOfWork.SaveChangesAsync();

                return result;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<IEnumerable<Produto>> PesquisarProdutos(string nome)
        {
            return await _produtoRepository.PesquisarProdutos(nome);
        }

        public async Task<Produto> AtualizarProduto(Produto produto)
        {
            try
            {
                _produtoRepository.AtualizarProduto(produto);
                await _produtoRepository.UnitOfWork.SaveChangesAsync();

                return produto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<bool> ExcluirProduto(Produto produto)
        {
            try
            {
                var result = _produtoRepository.ExcluirProduto(produto);
                await _produtoRepository.UnitOfWork.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        public async Task<Imagem> GetProdutoImagemById(Guid produtoId, long imagemId)
        {
            return await _produtoRepository.GetProdutoImagemById(produtoId, imagemId);
        }
    }
}