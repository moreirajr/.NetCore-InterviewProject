using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Produtos.Domain.Models;
using Produtos.Domain.SeedWork;
using Produtos.Infra.Data.EF.Mappings;

namespace Produtos.Infra.Data.EF
{
    public class ProdutoDbContext : DbContext, IUnitOfWork
    {
        public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options, bool ensureCreatedDb = true) : base(options)
        {
            if (ensureCreatedDb)
                CreateDatabaseIfNotExists();

            ChangeTracker.LazyLoadingEnabled = false;
        }

        private void CreateDatabaseIfNotExists()
        {
            if (!(Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                Database.EnsureCreated();
            }
        }

        #region Model Creation
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new ImagemMap());
        }
        #endregion


        #region DB Sets
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        #endregion
    }
}