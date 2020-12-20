using Produtos.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Produtos.Domain.Models
{
    public class Produto : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public virtual ICollection<Imagem> Imagens { get; set; }

        public Produto() 
        {
            Imagens = new List<Imagem>();
        }

        public Produto(string nome, decimal valor) : this()
        {
            Id = Guid.NewGuid();
            Nome = string.IsNullOrEmpty(nome) ? throw new ArgumentNullException("Campo nome obrigatório.") : nome;
            Valor = valor <= 0 ? throw new ArgumentNullException("Campo valor deve ser maior ou igual a 1.") : valor;
        }

        public void AdicionarImagem(Imagem img)
        {
            Imagens.Add(img);
        }

        public void AtualizarProduto(string nome, decimal valor)
        {
            Nome = nome;
            Valor = valor;
        }
    }
}