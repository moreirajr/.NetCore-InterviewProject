using Microsoft.EntityFrameworkCore;
using Produtos.Domain.Interfaces;
using Produtos.Domain.Models;
using Produtos.Domain.SeedWork;
using Produtos.Infra.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.Infra.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoDbContext _context;

        public ProdutoRepository(ProdutoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Produto> GetById(Guid id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Produto> CadastrarProduto(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);

            return produto;
        }

        public async Task<IEnumerable<Produto>> PesquisarProdutos(string nome)
        {
            var query = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(x => x.Nome.Contains(nome));

            return await query.Include(x => x.Imagens).ToListAsync();
        }

        public Produto AtualizarProduto(Produto produto)
        {
            _context.Produtos.Update(produto);

            return produto;
        }

        public bool ExcluirProduto(Produto produto)
        {
            return (_context.Produtos.Remove(produto).State == EntityState.Deleted);
        }

        public async Task<Imagem> GetProdutoImagemById(Guid produtoId, long imagemId)
        {
            return await _context.Imagens.FirstOrDefaultAsync(x => x.Id == imagemId && x.ProdutoId == produtoId);
        }
    }
}