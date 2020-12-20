using System;
using System.Collections.Generic;

namespace Produtos.Application.Produtos.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public IEnumerable<string> Imagens { get; set; }
    }
}