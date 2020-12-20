using System;

namespace Produtos.Domain.Models
{
    public class Imagem
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public Imagem() { }

        public Imagem(string nome)
        {
            Nome = string.IsNullOrEmpty(nome) ? throw new ArgumentNullException("Campo nome obrigatório.") : nome;
        }
    }
}