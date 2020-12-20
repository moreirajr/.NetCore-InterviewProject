using System;

namespace Produtos.Application.Produtos.ViewModels
{
    public class ImagemViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public Guid ProdutoId { get; set; }

        public string NomeImagemSalva => $"{ProdutoId}_{Nome}";
    }
}