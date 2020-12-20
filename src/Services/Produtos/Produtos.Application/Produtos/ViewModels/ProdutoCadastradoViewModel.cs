using System;

namespace Produtos.Application.Produtos.ViewModels
{
    public class ProdutoCadastradoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}