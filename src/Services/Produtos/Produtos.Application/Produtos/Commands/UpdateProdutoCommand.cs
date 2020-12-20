using System;
using System.ComponentModel.DataAnnotations;

namespace Produtos.Application.Produtos.Commands
{
    public class UpdateProdutoCommand
    {
        [Required(ErrorMessage = "Id obrigatório")]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Nome obrigatório")]
        public string Nome { get; set; }
        [Range(1, 9999999.99, ErrorMessage = "Valor do produto deve ser maior que 0")]
        public decimal Valor { get; set; }
    }
}